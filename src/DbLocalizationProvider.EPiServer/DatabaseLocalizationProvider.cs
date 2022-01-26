// Copyright (c) Valdis Iljuconoks. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DbLocalizationProvider.Queries;

namespace DbLocalizationProvider.EPiServer
{
    public class DatabaseLocalizationProvider : global::EPiServer.Framework.Localization.LocalizationProvider
    {
        private readonly ConfigurationContext _context;
        private readonly IQueryExecutor _queryExecutor;

        public DatabaseLocalizationProvider(ConfigurationContext context, IQueryExecutor queryExecutor)
        {
            _context = context;
            _queryExecutor = queryExecutor;
        }

        public override IEnumerable<CultureInfo> AvailableLanguages
        {
            get
            {
                return _queryExecutor
                    .Execute(new AvailableLanguages.Query())
                    .Select(al => al.CultureInfo);
            }
        }

        public override string GetString(string originalKey, string[] normalizedKey, CultureInfo culture)
        {
            // this is special case for Episerver ;)
            // https://world.episerver.com/forum/developer-forum/-Episerver-75-CMS/Thread-Container/2019/10/takes-a-lot-of-time-for-epi-cms-resources-to-load-on-dxc-service/

            return _context.ShouldLookupResource(originalKey)
                ? _queryExecutor.Execute(new GetTranslation.Query(originalKey, culture))
                : null;
        }

        public override IEnumerable<global::EPiServer.Framework.Localization.ResourceItem> GetAllStrings(
            string originalKey,
            string[] normalizedKey,
            CultureInfo culture)
        {
            // this is special case for Episerver ;)
            // https://world.episerver.com/forum/developer-forum/-Episerver-75-CMS/Thread-Container/2019/10/takes-a-lot-of-time-for-epi-cms-resources-to-load-on-dxc-service/
            if (!_context.ShouldLookupResource(originalKey))
            {
                return Enumerable.Empty<global::EPiServer.Framework.Localization.ResourceItem>();
            }

            var q = new GetAllResources.Query();
            var allResources = _queryExecutor
                               .Execute(q)
                               .Where(r =>
                                   r.ResourceKey.StartsWith(originalKey)
                                   && r.Translations != null
                                   && r.Translations.Any(t => t.Language == culture.Name))
                               .ToList();

            if (!allResources.Any())
            {
                return Enumerable.Empty<global::EPiServer.Framework.Localization.ResourceItem>();
            }

            return allResources
                   .Select(r => new global::EPiServer.Framework.Localization.ResourceItem(
                       r.ResourceKey,
                       r.Translations.FindByLanguage(culture)?.Value,
                       culture))
                   .ToList();
        }
    }
}
