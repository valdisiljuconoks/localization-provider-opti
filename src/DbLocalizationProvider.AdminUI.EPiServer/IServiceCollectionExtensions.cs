// Copyright (c) Valdis Iljuconoks. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System;
using System.Linq;
using DbLocalizationProvider.AdminUI.AspNetCore;
using EPiServer.Authorization;
using EPiServer.Cms.Shell;
using EPiServer.Shell.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace DbLocalizationProvider.AdminUI.EPiServer
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddEpiserverDbLocalizationProviderAdminUI(
            this IServiceCollection services,
            Action<UiConfigurationContext> setup = null)
        {
            services.AddCmsUI();
            services.Configure<ProtectedModuleOptions>(
                pm =>
                {
                    if (!pm.Items.Any(i => i.Name.Equals("DbLocalizationProvider.AdminUI.EPiServer", StringComparison.OrdinalIgnoreCase)))
                    {
                        pm.Items.Add(new ModuleDetails { Name = "DbLocalizationProvider.AdminUI.EPiServer" });
                    }
                });

            services.AddDbLocalizationProviderAdminUI(setup);

            return services;
        }
    }
}
