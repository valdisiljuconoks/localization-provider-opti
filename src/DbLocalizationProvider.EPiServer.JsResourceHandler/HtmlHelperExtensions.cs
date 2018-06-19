// Copyright (c) 2018 Valdis Iljuconoks.
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using DbLocalizationProvider.Internal;
using ExpressionHelper = DbLocalizationProvider.Internal.ExpressionHelper;

namespace DbLocalizationProvider.EPiServer.JsResourceHandler
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString GetTranslations<TModel>(this HtmlHelper<TModel> helper, Type containerType, string language = null, string alias = null, bool debug = false, bool camelCase = false)
        {
            return GetTranslations((HtmlHelper)helper, containerType, language, alias, debug, camelCase);
        }

        public static MvcHtmlString GetTranslations(this HtmlHelper helper, Type containerType, string language = null, string alias = null, bool debug = false, bool camelCase = false)
        {
            if(containerType == null)
                throw new ArgumentNullException(nameof(containerType));

            return GenerateScriptTag(language, alias, debug, ResourceKeyBuilder.BuildResourceKey(containerType), camelCase);
        }

        public static MvcHtmlString GetTranslations(this HtmlHelper helper, Expression<Func<object>> model, string language = null, string alias = null, bool debug = false, bool camelCase = false)
        {
            if(model == null)
                throw new ArgumentNullException(nameof(model));

            return GenerateScriptTag(language, alias, debug, ExpressionHelper.GetFullMemberName(model), camelCase);
        }

        private static MvcHtmlString GenerateScriptTag(string language, string alias, bool debug, string resourceKey, bool camelCase)
        {
            // if 1st request
            var mergeScript = string.Empty;
            if(HttpContext.Current?.Items["__DbLocalizationProvider_JsHandler_1stRequest"] == null)
            {
                HttpContext.Current?.Items.Add("__DbLocalizationProvider_JsHandler_1stRequest", false);
                mergeScript = $"<script src=\"/{Constants.PathBase}/{Constants.DeepMergeScriptName}\"></script>";
            }

            var url = $"/{Constants.PathBase}/{resourceKey.Replace("+", "---")}";
            var parameters = new Dictionary<string, string>();

            if(!string.IsNullOrEmpty(language))
                parameters.Add("lang", language);

            if(!string.IsNullOrEmpty(alias))
                parameters.Add("alias", alias);

            if(debug)
                parameters.Add("debug", "true");

            if(camelCase)
                parameters.Add("camel", "true");

            if(parameters.Any())
                url += "?" + ToQueryString(parameters);

            return new MvcHtmlString($"{mergeScript}<script src=\"{url}\"></script>");
        }

        private static string ToQueryString(Dictionary<string, string> parameters)
        {
            if(parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            if(!parameters.Any())
                return string.Empty;

            return string.Join("&", parameters.Select(kv => $"{kv.Key}={kv.Value}"));
        }
    }
}
