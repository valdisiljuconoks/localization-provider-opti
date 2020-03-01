// Copyright (c) Valdis Iljuconoks. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using EPiServer.Core;
using Helper = DbLocalizationProvider.JsResourceHandler.HtmlHelperExtensions;

namespace DbLocalizationProvider.EPiServer.JsResourceHandler
{
    // NOTE: This class is here only for backward compatibility
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString GetTranslations<TModel>(this HtmlHelper<TModel> helper, Type containerType, string language = null, string alias = null, bool debug = false, bool camelCase = false)
        {
            return GetTranslations((HtmlHelper)helper, containerType, language, alias, debug, camelCase);
        }

        public static MvcHtmlString GetTranslations(this HtmlHelper helper, Type containerType, string language = null, string alias = null, bool debug = false, bool camelCase = false)
        {
            return Helper.GetTranslations(helper, containerType, language ?? LanguageSelector.AutoDetect().Language.Name, alias, debug, camelCase);
        }

        public static MvcHtmlString GetTranslations(this HtmlHelper helper, Expression<Func<object>> model, string language = null, string alias = null, bool debug = false, bool camelCase = false)
        {
            return Helper.GetTranslations(helper, model, language ?? LanguageSelector.AutoDetect().Language.Name, alias, debug, camelCase);
        }
    }
}
