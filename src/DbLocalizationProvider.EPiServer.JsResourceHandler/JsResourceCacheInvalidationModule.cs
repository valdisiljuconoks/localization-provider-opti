// Copyright (c) Valdis Iljuconoks. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Web;
using DbLocalizationProvider.Cache;
using EPiServer;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using CacheKeyHelper = DbLocalizationProvider.JsResourceHandler.CacheKeyHelper;
using InitializationModule = EPiServer.Web.InitializationModule;

namespace DbLocalizationProvider.EPiServer.JsResourceHandler
{
    [InitializableModule]
    [ModuleDependency(typeof(InitializationModule))]
    public class JsResourceCacheInvalidationModule : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            ConfigurationContext.Current.CacheManager.OnRemove += CacheManagerOnOnRemove;
        }

        public void Uninitialize(InitializationEngine context)
        {
            ConfigurationContext.Current.CacheManager.OnRemove -= CacheManagerOnOnRemove;
        }

        private void CacheManagerOnOnRemove(CacheEventArgs cacheEventArgs)
        {
            var existingKeys = HttpContext.Current.Cache.GetEnumerator();
            var entriesToRemove = new List<string>();

            while (existingKeys.MoveNext())
            {
                var key = existingKeys.Key?.ToString();
                var existingKey = CacheKeyHelper.GetContainerName(key);
                if (existingKey != null && cacheEventArgs.ResourceKey.StartsWith(existingKey, StringComparison.InvariantCultureIgnoreCase))
                {
                    entriesToRemove.Add(key);
                }
            }

            foreach (var entry in entriesToRemove)
            {
                CacheManager.Remove(entry);
            }
        }
    }
}
