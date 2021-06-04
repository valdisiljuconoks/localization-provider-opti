// Copyright (c) Valdis Iljuconoks. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System;
using System.Globalization;
using System.Linq.Expressions;
using DbLocalizationProvider.Internal;
using EPiServer.Framework.Localization;
using EPiServer.ServiceLocation;
using Microsoft.Extensions.DependencyInjection;

namespace DbLocalizationProvider.EPiServer
{
    public static class LocalizationServiceExtensions
    {
        public static string GetString(this LocalizationService service, Expression<Func<object>> resource, params object[] formatArguments)
        {
            return GetStringByCulture(service, resource, CultureInfo.CurrentUICulture, formatArguments);
        }

        public static string GetStringByCulture(this LocalizationService service, Expression<Func<object>> resource, CultureInfo culture, params object[] formatArguments)
        {
            var helper = ServiceLocator.Current.GetRequiredService<ExpressionHelper>();
            var resourceKey = helper.GetFullMemberName(resource);

            return GetStringByCulture(service, resourceKey, culture, formatArguments);
        }

        public static string GetStringByCulture(this LocalizationService service, string resourceKey, CultureInfo culture, params object[] formatArguments)
        {
            var provider = ServiceLocator.Current.GetRequiredService<LocalizationProvider>();

            return provider.GetStringByCulture(resourceKey, culture, formatArguments);
        }
    }
}
