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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DbLocalizationProvider.Abstractions;
using DbLocalizationProvider.AspNet.Queries;
using DbLocalizationProvider.EPiServer.Queries;
using DbLocalizationProvider.Queries;
using EPiServer.Framework.Localization;
using EPiServer.Logging;

namespace DbLocalizationProvider.EPiServer
{
    public class DatabaseLocalizationProvider : global::EPiServer.Framework.Localization.LocalizationProvider
    {
        private readonly ILogger _logger;

        // we have to move to Lazy<T> because configuration settings will not be set during DatabaseProvider constructor
        // as this type is created pretty early in the pipeline and none of initialization modules have been run yet
        private readonly Lazy<IQueryHandler<GetTranslation.Query, string>> _originalHandler
            = new Lazy<IQueryHandler<GetTranslation.Query, string>>(() =>
                                                                    {
                                                                        if(ConfigurationContext.Current.DiagnosticsEnabled)
                                                                            return new EPiServerGetTranslation.HandlerWithLogging(new GetTranslationHandler());

                                                                        return new GetTranslationHandler();
                                                                    });

        public DatabaseLocalizationProvider()
        {
            _logger = LogManager.GetLogger(typeof(DatabaseLocalizationProvider));
        }

        public override IEnumerable<CultureInfo> AvailableLanguages => new AvailableLanguages.Query().Execute();

        public override string GetString(string originalKey, string[] normalizedKey, CultureInfo culture)
        {
            _logger.Debug($"Executing query for resource key `{originalKey}` for language: `{culture.Name}...");

            // we need to call handler directly here
            // if we would dispatch query and ask registered handler to execute
            // we would end up in stack-overflow as in EPiServer context
            // the same database localization provider is registered as the query handler
            var foundTranslation = _originalHandler.Value.Execute(new GetTranslation.Query(originalKey, culture, false));

            // this is last chance for Episerver to find translation (asked in translation fallback language)
            // if no match is found and invariant fallback is configured - return invariant culture translation
            if(foundTranslation == null
               && LocalizationService.Current.FallbackBehavior.HasFlag(FallbackBehaviors.FallbackCulture)
               && ConfigurationContext.Current.EnableInvariantCultureFallback
               && (Equals(culture, LocalizationService.Current.FallbackCulture) || Equals(culture.Parent, CultureInfo.InvariantCulture)))
            {
                _logger.Debug($"Null returned for resource key `{originalKey}` for language: `{culture.Name} Executing InvariantCulture fallback.");
                return _originalHandler.Value.Execute(new GetTranslation.Query(originalKey, CultureInfo.InvariantCulture, false));
            }

            return foundTranslation;
        }

        public override IEnumerable<global::EPiServer.Framework.Localization.ResourceItem> GetAllStrings(string originalKey, string[] normalizedKey, CultureInfo culture)
        {
            var q = new GetAllTranslations.Query(originalKey, culture);

            return q.Execute()
                    .Select(r => new global::EPiServer.Framework.Localization.ResourceItem(r.Key, r.Value, r.SourceCulture));
        }
    }
}
