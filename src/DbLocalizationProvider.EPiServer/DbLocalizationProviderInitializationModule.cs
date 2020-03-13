// Copyright (c) Valdis Iljuconoks. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using DbLocalizationProvider.DataAnnotations;
using DbLocalizationProvider.EPiServer.Queries;
using DbLocalizationProvider.Queries;
using EPiServer.Data;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Framework.Localization;
using EPiServer.ServiceLocation;
using InitializationModule = EPiServer.Web.InitializationModule;

namespace DbLocalizationProvider.EPiServer
{
    [InitializableModule]
    [ModuleDependency(typeof(InitializationModule))]
    public class DbLocalizationProviderInitializationModule : IConfigurableModule
    {
        private ServiceConfigurationContext _context;
        private bool _eventHandlerAttached;
        private InitializationEngine _engine;

        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            // we need to capture original context in order to replace ModelMetaDataProvider later if needed
            _context = context;
        }

        public void Initialize(InitializationEngine context)
        {
            if(_eventHandlerAttached) return;

            _engine = context;
            context.InitComplete += SetupLocalizationProvider;
            _eventHandlerAttached = true;
        }

        public void Uninitialize(InitializationEngine context)
        {
            context.InitComplete -= SetupLocalizationProvider;
        }

        private void SetupLocalizationProvider(object sender, EventArgs eventArgs)
        {
            ConfigurationContext.Current.Logger = new LoggerAdapter();

            // reuse the same ASP.NET setup method with some quirks
            IAppBuilderExtensions.UseDbLocalizationProvider(null, ctx =>
            {
                ctx.CacheManager = new EPiServerCacheManager();

                ctx.TypeScanners.Insert(0, new LocalizedCategoryScanner());

                ctx.TypeFactory.ForQuery<AvailableLanguages.Query>().SetHandler<EPiServerAvailableLanguages.Handler>();
                ctx.TypeFactory.ForQuery<DetermineDefaultCulture.Query>().SetHandler<EPiServerDetermineDefaultCulture.Handler>();

                // respect configuration whether we should sync and register resources
                // skip if application currently is in read-only mode
                var dbMode = _engine.Locate.Advanced.GetInstance<IDatabaseMode>().DatabaseMode;
                if (ctx.DiscoverAndRegisterResources) ctx.DiscoverAndRegisterResources = dbMode != DatabaseMode.ReadOnly;

                // if fallback list is empty - meaning that user has not configured anything
                // we can jump in and initialize config from Episerver settings
                if (!ctx.FallbackCultures.Any())
                {
                    // read language fallback from the configuration file
                    if (LocalizationService.Current.FallbackBehavior.HasFlag(FallbackBehaviors.FallbackCulture)
                    && !Equals(LocalizationService.Current.FallbackCulture, CultureInfo.InvariantCulture))
                    {
                        ctx.FallbackCultures.Try(LocalizationService.Current.FallbackCulture);
                    }
                }

                // also we need to make sure that invariant culture is last in the list of fallback to invariant is true
                if (ctx.EnableInvariantCultureFallback) ctx.FallbackCultures.Then(CultureInfo.InvariantCulture);

                // register also model metadata providers
                // it's a bit different for Episerver compared to default ASP.NET implementation
                // In Episerver - providers are located in IoC (instead of `ModelMetadataProviders.Current` in case with ASP.NET)
                ctx.ModelMetadataProviders.SetupCallback = () =>
                {
                    if (!_context.Services.Contains(typeof(ModelMetadataProvider)))
                    {
                        // set new provider
                        if (ConfigurationContext.Current.ModelMetadataProviders.UseCachedProviders)
                            _context.Services.AddSingleton<ModelMetadataProvider, CachedLocalizedMetadataProvider>();
                        else
                            _context.Services.AddSingleton<ModelMetadataProvider, LocalizedMetadataProvider>();
                    }
                    else
                    {
                        var currentProvider = ServiceLocator.Current.GetInstance<ModelMetadataProvider>();

                        // decorate existing provider
                        if (ConfigurationContext.Current.ModelMetadataProviders.UseCachedProviders)
                            _context.Services.AddSingleton<ModelMetadataProvider>(new CompositeModelMetadataProvider<CachedLocalizedMetadataProvider>(currentProvider));
                        else
                            _context.Services.AddSingleton<ModelMetadataProvider>(new CompositeModelMetadataProvider<LocalizedMetadataProvider>(currentProvider));
                    }

                    for (var i = 0; i < ModelValidatorProviders.Providers.Count; i++)
                    {
                        var provider = ModelValidatorProviders.Providers[i];
                        if (!(provider is DataAnnotationsModelValidatorProvider)) continue;

                        ModelValidatorProviders.Providers.RemoveAt(i);
                        ModelValidatorProviders.Providers.Insert(i, new LocalizedModelValidatorProvider());
                        break;
                    }
                };
            });

            // make sure that Episerver knows about LocalizationProvider
            _context.Services.AddSingleton(LocalizationProvider.Current);
            _context.Services.AddSingleton<ILocalizationProvider>(LocalizationProvider.Current);
        }
    }
}
