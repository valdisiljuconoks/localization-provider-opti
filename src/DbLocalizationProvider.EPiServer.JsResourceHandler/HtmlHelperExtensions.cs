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
