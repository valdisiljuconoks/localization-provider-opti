// Copyright (c) Mattias Olsson, Valdis Iljuconoks. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DbLocalizationProvider.Csv;
using DbLocalizationProvider.EPiServer;
using EPiServer.DataAbstraction;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;

namespace DbLocalizationProvider.AdminUI.EPiServer.Csv
{
    [InitializableModule]
    [ModuleDependency(typeof(InitializationModule))]
    [ModuleDependency(typeof(ServiceContainerInitialization))]
    [ModuleDependency(typeof(DbLocalizationProviderInitializationModule))]
    public class SetupCsvProvider : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var locator = context.Locate.Advanced;
            var languageBranchRepository = locator.GetInstance<ILanguageBranchRepository>();

            ICollection<CultureInfo> LanguagesFactory() => languageBranchRepository
                                                           .ListEnabled()
                                                           .Select(x => x.Culture)
                                                           .ToList();

            ConfigurationContext.Setup(ctx =>
                                       {
                                           ctx.Export.Providers.Add(new CsvResourceExporter(LanguagesFactory));
                                           ctx.Import.Providers.Add(new CsvResourceFormatParser(LanguagesFactory));
                                       });
        }

        public void Uninitialize(InitializationEngine context) { }
    }
}
