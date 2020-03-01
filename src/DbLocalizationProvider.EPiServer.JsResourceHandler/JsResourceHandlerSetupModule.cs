// Copyright (c) Valdis Iljuconoks. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System.Web.Mvc;
using System.Web.Routing;
using DbLocalizationProvider.JsResourceHandler;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Framework.Localization;

namespace DbLocalizationProvider.EPiServer.JsResourceHandler
{
    [InitializableModule]
    public class JsResourceHandlerSetupModule : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            RouteTable.Routes.IgnoreRoute(Constants.IgnoreRoute);

            // read fallback language from Episerver settings
            ConfigurationContext.Current.FallbackCultures.Add(context.Locate.Advanced.GetInstance<LocalizationService>().FallbackCulture);
        }

        public void Uninitialize(InitializationEngine context) { }
    }
}
