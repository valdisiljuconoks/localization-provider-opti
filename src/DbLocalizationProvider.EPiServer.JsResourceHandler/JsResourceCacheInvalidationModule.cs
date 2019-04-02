// Copyright (c) 2019 Valdis Iljuconoks.
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

using System.Collections.Generic;
using System.Web;
using DbLocalizationProvider.Cache;
using EPiServer;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
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
                var existingKey = DbLocalizationProvider.JsResourceHandler.CacheKeyHelper.GetContainerName(key);
                if(existingKey!= null && cacheEventArgs.ResourceKey.StartsWith(existingKey))
                    entriesToRemove.Add(key);
            }

            foreach (var entry in entriesToRemove)
                CacheManager.Remove(entry);
        }
    }
}
