// Copyright (c) Valdis Iljuconoks. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web;

namespace DbLocalizationProvider.AdminUI.EPiServer
{
    [ModuleDependency(typeof(InitializationModule))]
    public class InitializationModule1 : IConfigurableModule
    {
        public void Initialize(InitializationEngine context) { }

        public void Uninitialize(InitializationEngine context) { }

        public void ConfigureContainer(ServiceConfigurationContext context) { context.Services.AddLocalizationProviderAdminUI(); }
    }
}
