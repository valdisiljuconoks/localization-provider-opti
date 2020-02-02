using System.Data.SqlClient;
using System.IO;
using DbLocalizationProvider.Storage.SqlServer;
using DbLocalizationProvider.Sync;

namespace DbLocalizationProvider.MigrationTool
{
    internal class ResourceImporter
    {
        public void Import(MigrationToolOptions settings)
        {
            var sourceImportFilePath = Path.Combine(settings.SourceDirectory, "localization-resource-translations.sql");
            if (!File.Exists(sourceImportFilePath))
            {
                throw new IOException($"Source file '{sourceImportFilePath}' for import not found!");
            }

            // create DB structures in target database
            var updater = new SchemaUpdater();
            updater.Execute(new UpdateSchema.Command());

            var fileInfo = new FileInfo(sourceImportFilePath);
            var script = fileInfo.OpenText().ReadToEnd();
            using (var connection = new SqlConnection(settings.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(script, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
