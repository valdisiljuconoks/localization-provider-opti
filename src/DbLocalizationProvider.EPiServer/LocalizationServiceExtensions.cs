// Copyright (c) Valdis Iljuconoks. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System;
using System.Globalization;
using System.Linq.Expressions;
using DbLocalizationProvider.Internal;
using DbLocalizationProvider.Sync;
using EPiServer.Framework.Localization;
using EPiServer.ServiceLocation;

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
            var helper = TryGetExpressionHelper();
            var resourceKey = helper.GetFullMemberName(resource);

            return GetStringByCulture(service, resourceKey, culture, formatArguments);
        }

        public static string GetStringByCulture(this LocalizationService service, string resourceKey, CultureInfo culture, params object[] formatArguments)
        {
            var translation = service.GetStringByCulture(resourceKey, culture);
            if (string.IsNullOrEmpty(translation))
            {
                return translation;
            }

            return LocalizationProvider.Format(translation, formatArguments);
        }

        private static ExpressionHelper TryGetExpressionHelper()
        {

            if (ServiceLocator.Current.TryGetExistingInstance<ExpressionHelper>(out var helper))
            {
                return helper;
            }

            return new ExpressionHelper(new ResourceKeyBuilder(new ScanState()));
        }
    }
}
