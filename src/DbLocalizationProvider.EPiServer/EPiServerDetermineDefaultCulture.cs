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
            private readonly ConfigurationContext _context;

            public Handler(ConfigurationContext context)
            {
                _context = context;
            }

            public string Execute(DetermineDefaultCulture.Query query)
            {
                return _context.DefaultResourceCulture != null
                           ? _context.DefaultResourceCulture.Name
                           : ContentLanguage.PreferredCulture == null ? "en" : ContentLanguage.PreferredCulture.Name;
            }
        }
    }
}
