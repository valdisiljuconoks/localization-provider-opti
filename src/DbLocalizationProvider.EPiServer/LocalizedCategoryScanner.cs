// Copyright (c) Valdis Iljuconoks. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DbLocalizationProvider.Sync;
using EPiServer.DataAbstraction;

namespace DbLocalizationProvider.EPiServer
{
    public class LocalizedCategoryScanner : IResourceTypeScanner
    {
        public bool ShouldScan(Type target)
        {
            return typeof(Category).IsAssignableFrom(target)
                   && target.GetCustomAttribute<LocalizedCategoryAttribute>() != null;
        }

        public string GetResourceKeyPrefix(Type target, string keyPrefix = null)
        {
            return null;
        }

        public ICollection<DiscoveredResource> GetClassLevelResources(Type target, string resourceKeyPrefix)
        {
            var translation = target.Name;

            try
            {
                var category = (Activator.CreateInstance(target) as Category);
                if(!string.IsNullOrEmpty(category?.Name))
                {
                    translation = category.Name;
                }
            }
            catch(Exception) { }


            return new List<DiscoveredResource>
                   {
                       new DiscoveredResource(null,
                                              $"/categories/category[@name=\"{target.Name}\"]/description",
                                              DiscoveredTranslation.FromSingle(translation),
                                              target.Name,
                                              target,
                                              typeof(string),
                                              true)
                   };
        }

        public ICollection<DiscoveredResource> GetResources(Type target, string resourceKeyPrefix)
        {
            return Enumerable.Empty<DiscoveredResource>().ToList();
        }
    }
}
