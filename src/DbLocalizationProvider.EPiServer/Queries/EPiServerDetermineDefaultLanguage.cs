// Copyright (c) Valdis Iljuconoks. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using DbLocalizationProvider.Abstractions;
using DbLocalizationProvider.Queries;
using EPiServer.Core;

namespace DbLocalizationProvider.EPiServer.Queries
{
    public class EPiServerDetermineDefaultLanguage
    {
        public class Handler : IQueryHandler<DetermineDefaultCulture.Query, string>
        {
            public string Execute(DetermineDefaultCulture.Query query)
            {
                return LanguageSelector.AutoDetect().Language.Name;
            }
        }
    }
}
