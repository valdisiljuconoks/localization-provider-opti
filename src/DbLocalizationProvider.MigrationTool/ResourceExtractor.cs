using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;

namespace DbLocalizationProvider.MigrationTool
{
    internal class ResourceExtractor
    {
        internal ICollection<LocalizationResource> Extract(MigrationToolOptions settings)
        {
            ICollection<LocalizationResource> resources = new List<LocalizationResource>();

            if(settings.ExportFromXmlOnly)
                resources = GetXmlResources(settings);

            if(settings.ExportFromDatabase)
            {
                using(var db = new LanguageEntities(settings.ConnectionString))
                {
                    resources = db.LocalizationResources.Include(r => r.Translations).ToList();
                }

                InitializeDb(settings);
            }

            return resources;
        }

        private void InitializeDb(MigrationToolOptions settings)
        {
            // initialize DB - to generate data structures
            {
                try
                {
                    using(var db = new LanguageEntities(settings.ConnectionString))
                    {
                        var resource = db.LocalizationResources.Where(r => r.Id == 0);
                    }
                }
                catch
                {
                    // it's OK to have exception here
                }
            }
        }

        private ICollection<LocalizationResource> GetXmlResources(MigrationToolOptions settings)
        {
            // test few default conventions (lazy enough to read from EPiServer Framework configuration file)
            string resourceFilesSourceDir;
            if(!string.IsNullOrEmpty(settings.ResourceDirectory))
            {
                resourceFilesSourceDir = Path.Combine(settings.SourceDirectory, settings.ResourceDirectory);
            }
            else
            {
                resourceFilesSourceDir = Path.Combine(settings.SourceDirectory, "Resources\\LanguageFiles");
                if(!Directory.Exists(resourceFilesSourceDir))
                {
                    resourceFilesSourceDir = Path.Combine(settings.SourceDirectory, "lang");
                }
            }

            if(!Directory.Exists(resourceFilesSourceDir))
            {
                throw new IOException($"Default resource directory '{resourceFilesSourceDir}' does not exist or can't be found. Use `-resourceDirectory` argument");
            }

            var resourceFiles = Directory.GetFiles(resourceFilesSourceDir, "*.xml");
            if(!resourceFiles.Any())
            {
                Console.WriteLine($"No resource files found in '{resourceFilesSourceDir}'");
            }

            var fileProcessor = new ResourceFileProcessor(settings.IgnoreDuplicateKeys);

            return fileProcessor.ParseFiles(resourceFiles);
        }
    }
}
