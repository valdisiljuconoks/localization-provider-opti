// Copyright (c) Valdis Iljuconoks. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DbLocalizationProvider.Abstractions;
using DbLocalizationProvider.Queries;
using EPiServer.DataAbstraction;
using EPiServer.Security;

namespace DbLocalizationProvider.EPiServer.Queries
{
    public class EPiServerAvailableLanguages
    {
        public class Handler : IQueryHandler<AvailableLanguages.Query, IEnumerable<CultureInfo>>
        {
            private readonly ILanguageBranchRepository _languageBranchRepository;

            public Handler(ILanguageBranchRepository languageBranchRepository)
            {
                _languageBranchRepository = languageBranchRepository;
            }

            public IEnumerable<CultureInfo> Execute(AvailableLanguages.Query query)
            {
                var currentLanguages = _languageBranchRepository.ListEnabled()
                                                                .Where(l => l.QueryEditAccessRights(PrincipalInfo.CurrentPrincipal))
                                                                .OrderByDescending(l => l.Name)
                                                                .Select(l => new CultureInfo(l.LanguageID));

                if(query.IncludeInvariant)
                    currentLanguages = new[] { CultureInfo.InvariantCulture }.Concat(currentLanguages);

                return currentLanguages;
            }
        }
    }
}
