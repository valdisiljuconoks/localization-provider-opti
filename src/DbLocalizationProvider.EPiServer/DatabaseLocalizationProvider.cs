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
        public override IEnumerable<CultureInfo> AvailableLanguages => new AvailableLanguages.Query().Execute();

        public override string GetString(string originalKey, string[] normalizedKey, CultureInfo culture)
        {
            // this is special case for Episerver ;)
            // https://world.episerver.com/forum/developer-forum/-Episerver-75-CMS/Thread-Container/2019/10/takes-a-lot-of-time-for-epi-cms-resources-to-load-on-dxc-service/

            return ConfigurationContext.Current.ResourceLookupFilter(originalKey)
                ? new GetTranslation.Query(originalKey, culture).Execute()
                : null;
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
