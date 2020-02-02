using System.Configuration;
using DbLocalizationProvider.AspNet.Sync;
using DbLocalizationProvider.Storage.SqlServer;
using EPiServer.Data.Configuration;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;

namespace DbLocalizationProvider.EPiServer.Sample
{
    [InitializableModule]
    [ModuleDependency(typeof(ServiceContainerInitialization))]
    public class DbLocalizationProviderConnectionSetupModule : IConfigurableModule
    {
        public void Initialize(InitializationEngine context) { }

        public void Uninitialize(InitializationEngine context) { }

        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            ConfigurationContext.Setup(_ =>
            {
                _.ModelMetadataProviders.EnableLegacyMode = () => true;
                _.EnableInvariantCultureFallback = true;

                var connectionString = ConfigurationManager
                                       .ConnectionStrings[EPiServerDataStoreSection.Instance.DataSettings.ConnectionStringName]
                                       .ConnectionString;

                _.UseSqlServer(connectionString);
            });

            var sync = new Synchronizer();
            sync.UpdateStorageSchema();
        }
    }
}
