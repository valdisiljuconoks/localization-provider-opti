// Copyright (c) Valdis Iljuconoks. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using DbLocalizationProvider.EPiServer;
using DbLocalizationProvider.Xliff;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using InitializationModule = EPiServer.Web.InitializationModule;

namespace DbLocalizationProvider.AdminUI.EPiServer.Xliff
{
    [InitializableModule]
    [ModuleDependency(typeof(InitializationModule))]
    [ModuleDependency(typeof(DbLocalizationProviderInitializationModule))]
    public class SetupXliffProvider : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            ConfigurationContext.Setup(ctx =>
                                       {
                                           ctx.Export.Providers.Add(new Exporter());
                                           ctx.Import.Providers.Add(new FormatParser());
                                       });
        }

        public void Uninitialize(InitializationEngine context) { }
    }
}
