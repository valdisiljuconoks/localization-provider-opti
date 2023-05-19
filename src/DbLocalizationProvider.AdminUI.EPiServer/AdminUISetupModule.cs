// Copyright (c) Valdis Iljuconoks. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System;
using DbLocalizationProvider.EPiServer;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using InitializationModule = EPiServer.Web.InitializationModule;

namespace DbLocalizationProvider.AdminUI.EPiServer
{
    [InitializableModule]
    [ModuleDependency(typeof(InitializationModule))]
    [ModuleDependency(typeof(DbLocalizationProviderInitializationModule))]
    public class AdminUISetupModule : IInitializableModule
    {
        private static readonly string[] _adminRoles = { "CmsAdmins", "WebAdmins", "LocalizationAdmins" };

        private static readonly string[] _editorRoles =
        {
            "CmsEditors", "WebEditors", "LocalizationEditors", "CmsAdmins", "WebAdmins", "LocalizationAdmins"
        };

        private bool _eventHandlerAttached;

        public void Initialize(InitializationEngine context)
        {
            if (_eventHandlerAttached) return;

            context.InitComplete += FinalizeSetup;
            _eventHandlerAttached = true;
        }

        public void Uninitialize(InitializationEngine context)
        {
            context.InitComplete -= FinalizeSetup;
        }

        private void FinalizeSetup(object sender, EventArgs e)
        {
            if (!((IDirtyList)UiConfigurationContext.Current.AuthorizedAdminRoles).IsDirty)
            {
                foreach (var role in _adminRoles)
                {
                    UiConfigurationContext.Current.AuthorizedAdminRoles.Add(role);
                }
            }

            if (!((IDirtyList)UiConfigurationContext.Current.AuthorizedEditorRoles).IsDirty)
            {
                foreach (var role in _editorRoles)
                {
                    UiConfigurationContext.Current.AuthorizedEditorRoles.Add(role);
                }
            }
        }
    }
}
