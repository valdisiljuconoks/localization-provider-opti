// Copyright (c) Valdis Iljuconoks. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DbLocalizationProvider.Abstractions;
using DbLocalizationProvider.AspNet.Queries;
using DbLocalizationProvider.EPiServer.Queries;
using DbLocalizationProvider.Logging;
using DbLocalizationProvider.Queries;
using EPiServer.Framework.Localization;

namespace DbLocalizationProvider.EPiServer
{
    public class DatabaseLocalizationProvider : global::EPiServer.Framework.Localization.LocalizationProvider
    {
        private readonly ILogger _logger;

        // we have to move to Lazy<T> because configuration settings will not be set during DatabaseProvider constructor
        // as this type is created pretty early in the pipeline and none of initialization modules have been run yet
        private readonly Lazy<IQueryHandler<GetTranslation.Query, string>> _originalHandler
            = new Lazy<IQueryHandler<GetTranslation.Query, string>>(() =>
            {
                if (ConfigurationContext.Current.DiagnosticsEnabled)
                    return new EPiServerGetTranslation.HandlerWithLogging(new GetTranslationHandler());

                return new GetTranslationHandler();
            });

        public DatabaseLocalizationProvider()
        {
            _logger = ConfigurationContext.Current.Logger;
        }

        public override IEnumerable<CultureInfo> AvailableLanguages => new AvailableLanguages.Query().Execute();

        public override string GetString(string originalKey, string[] normalizedKey, CultureInfo culture)
        {
            // this is special case for Episerver ;)
            // https://world.episerver.com/forum/developer-forum/-Episerver-75-CMS/Thread-Container/2019/10/takes-a-lot-of-time-for-epi-cms-resources-to-load-on-dxc-service/
            if (!ConfigurationContext.Current.ResourceLookupFilter(originalKey)) return null;

            _logger.Debug($"Executing query for resource key `{originalKey}` for language: `{culture.Name}`...");

            // NOTE #1:
            //      we need to call handler directly here
            //      if we would dispatch query and ask registered handler to execute
            //      we would end up in stack-overflow as in EPiServer context
            //      the same database localization provider is registered as the query handler
            // NOTE #2:
            //      we also need to check for null and if null was returned - this means that resource does not exist in any language
            //      all fallback & invariant including (if configured)
            //      so we must tell EPiServer to stop falling back to other languages for this resource.
            //      this is done by returning empty string.
            var foundTranslation = _originalHandler.Value.Execute(new GetTranslation.Query(originalKey, culture)) ?? string.Empty;

            return foundTranslation;
        }

        public override IEnumerable<global::EPiServer.Framework.Localization.ResourceItem> GetAllStrings(
            string originalKey,
            string[] normalizedKey,
            CultureInfo culture)
        {
            // this is special case for Episerver ;)
            // https://world.episerver.com/forum/developer-forum/-Episerver-75-CMS/Thread-Container/2019/10/takes-a-lot-of-time-for-epi-cms-resources-to-load-on-dxc-service/
            if (!ConfigurationContext.Current.ResourceLookupFilter(originalKey))
            {
                return Enumerable.Empty<global::EPiServer.Framework.Localization.ResourceItem>();
            }

            var q = new GetAllTranslations.Query(originalKey, culture);

            return q.Execute()
                    .Select(r => new global::EPiServer.Framework.Localization.ResourceItem(r.Key, r.Value, r.SourceCulture));
        }
    }
}
