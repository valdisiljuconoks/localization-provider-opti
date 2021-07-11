// Copyright (c) Valdis Iljuconoks. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System;
using System.Globalization;
using System.Linq;
using DbLocalizationProvider.AspNetCore;
using DbLocalizationProvider.EPiServer.Queries;
using DbLocalizationProvider.Queries;
using EPiServer.Framework.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace DbLocalizationProvider.EPiServer
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddEpiserverDbLocalizationProvider(
            this IServiceCollection services,
            Action<ConfigurationContext> setup = null)
        {
            services.AddDbLocalizationProvider(ctx =>
            {
                ctx.CacheManager = new EPiServerCacheManager();

                // TODO: check if this gets injected properly
                //ctx.TypeScanners.Insert(0, new LocalizedCategoryScanner());

                ctx.TypeFactory.ForQuery<AvailableLanguages.Query>().SetHandler<EPiServerAvailableLanguages.Handler>();
                ctx.TypeFactory.ForQuery<DetermineDefaultCulture.Query>().SetHandler<EPiServerDetermineDefaultCulture.Handler>();

                // if fallback list is empty - meaning that user has not configured anything
                // we can jump in and initialize config from Episerver settings
                if (!ctx.FallbackLanguages.Any()
                    && LocalizationService.Current.FallbackBehavior.HasFlag(FallbackBehaviors.FallbackCulture)
                    && !Equals(LocalizationService.Current.FallbackCulture, CultureInfo.InvariantCulture))
                {
                    // read language fallback from the configuration file
                    ctx.FallbackLanguages.Try(LocalizationService.Current.FallbackCulture);
                }

                setup?.Invoke(ctx);

                services.AddLocalizationProvider<DatabaseLocalizationProvider>();
            });

            return services;
        }
    }
}
