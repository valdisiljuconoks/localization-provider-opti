using System;
using System.Linq;
using EPiServer.Authorization;
using EPiServer.Cms.Shell;
using EPiServer.Shell.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace DbLocalizationProvider.AdminUI.EPiServer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLocalizationProviderAdminUI(this IServiceCollection services)
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

            services.AddAuthorization(options =>
            {
                if (options.GetPolicy("episerver:localizationprovider:adminui") != null)
                {
                    return;
                }

                options.AddPolicy("episerver:localizationprovider:adminui", policy => policy.RequireRole(Roles.CmsAdmins));
            });

            return services;
        }
    }
}
