using System;
using DbLocalizationProvider.Sync;
using EPiServer.Data;
using EPiServer.Logging;
using EPiServer.ServiceLocation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DbLocalizationProvider.EPiServer
{
    /// <summary>
    /// Extension point to initialize provider.
    /// </summary>
    public static class InitializationExtensions
    {
        /// <summary>
        /// Synchronizes resources with underlying storage
        /// </summary>
        /// <param name="builder">ASP.NET Core application builder</param>
        /// <returns>ASP.NET Core application builder to enable fluent API call chains</returns>
        public static IApplicationBuilder UseEpiserverDbLocalizationProvider(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            UseDbLocalizationProvider(builder.ApplicationServices);

            return builder;
        }

        /// <summary>
        /// Synchronizes resources with underlying storage
        /// </summary>
        /// <param name="serviceFactory">Factory of the services (this will be required to get access to previously registered services)</param>
        /// <returns>ASP.NET Core application builder to enable fluent API call chains</returns>
        public static void UseDbLocalizationProvider(this IServiceProvider serviceFactory)
        {
            if (serviceFactory == null)
            {
                throw new ArgumentNullException(nameof(serviceFactory));
            }

            var context = serviceFactory.GetRequiredService<ConfigurationContext>();

            context.Logger = new LoggerAdapter();

            // respect configuration whether we should sync and register resources
            // skip if application currently is in read-only mode
            var dbMode = serviceFactory.GetRequiredService<IDatabaseMode>().DatabaseMode;
            if (context.DiscoverAndRegisterResources)
            {
                context.DiscoverAndRegisterResources = dbMode != DatabaseMode.ReadOnly;
            }

            // if we need to sync - then it's good time to do it now
            var sync = serviceFactory.GetRequiredService<Synchronizer>();
            sync.SyncResources(context.DiscoverAndRegisterResources);

            if (!context.DiscoverAndRegisterResources)
            {
                context.Logger.Info($"{nameof(context.DiscoverAndRegisterResources)}=false. Resource synchronization skipped.");
            }

            if (context.ManualResourceProvider != null)
            {
                sync.RegisterManually(context.ManualResourceProvider.GetResources());
            }

            context.Logger.Info("DbLocalizationProvider initialized.");
        }
    }
}
