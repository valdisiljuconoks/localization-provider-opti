// Copyright (c) Valdis Iljuconoks. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using DbLocalizationProvider.Abstractions;
using DbLocalizationProvider.Queries;
using EPiServer.Globalization;

namespace DbLocalizationProvider.EPiServer
{
    public class EPiServerDetermineDefaultCulture
    {
        public class Handler : IQueryHandler<DetermineDefaultCulture.Query, string>
        {
            public string Execute(DetermineDefaultCulture.Query query)
            {
                return ConfigurationContext.Current.DefaultResourceCulture != null
                           ? ConfigurationContext.Current.DefaultResourceCulture.Name
                           : (ContentLanguage.PreferredCulture != null ? ContentLanguage.PreferredCulture.Name : "en");
            }
        }
    }
}
