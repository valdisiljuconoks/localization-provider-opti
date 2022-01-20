// Copyright (c) Valdis Iljuconoks. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System.Globalization;
using DbLocalizationProvider.Abstractions;
using DbLocalizationProvider.Queries;
using EPiServer.Core;

namespace DbLocalizationProvider.EPiServer.Queries
{
    public class EPiServerGetCurrentUICulture
    {
        public class Handler : IQueryHandler<GetCurrentUICulture.Query, CultureInfo>
        {
            private readonly IUserInterfaceLanguageAccessor _accessor;

            public Handler(IUserInterfaceLanguageAccessor accessor)
            {
                _accessor = accessor;
            }

            public CultureInfo Execute(GetCurrentUICulture.Query query) => _accessor.Language;
        }
    }
}
