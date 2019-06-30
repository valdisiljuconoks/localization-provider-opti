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

using System.Globalization;
using DbLocalizationProvider.Abstractions;
using DbLocalizationProvider.AspNet.Queries;
using DbLocalizationProvider.Queries;
using EPiServer.Framework.Localization;
using EPiServer.Logging;
using EPiServer.ServiceLocation;

namespace DbLocalizationProvider.EPiServer.Queries
{
    public class EPiServerGetTranslation
    {
        public class Handler : IQueryHandler<GetTranslation.Query, string>
        {
            private readonly IQueryHandler<GetTranslation.Query, string> _originalHandler;
            private readonly ILogger _logger;

            public Handler()
            {
                _originalHandler = new GetTranslationHandler();
                _logger = LogManager.GetLogger(typeof(EPiServerGetTranslation));
            }

            public string Execute(GetTranslation.Query query)
            {
                _logger.Debug($"Executing query for resource key `{query.Key}` for language: `{query.Language.Name}...");

                var service = ServiceLocator.Current.GetInstance<LocalizationService>();
                var foundTranslation = service.GetStringByCulture(query.Key, query.Language);

                // * if asked for fallback language and there is no resource translation for that language
                //   Episerver gonna return me string.Empty - LocalizationService.GetMissingFallbackResourceValue()
                //   this is a bit more trickier that initially might look like
                // * if asked to return translation explicitly in InvariantCulture
                //   Episerver will understand this as whichever fallback language is configured
                //   and will pass in that language to look for translation
                //   so we must check this explicitly here
                if(string.IsNullOrEmpty(foundTranslation)
                   && service.FallbackBehavior.HasFlag(FallbackBehaviors.FallbackCulture)
                   && query.UseFallback
                   && (Equals(query.Language, service.FallbackCulture) || Equals(query.Language.Parent, CultureInfo.InvariantCulture)))
                {
                    // no translation found for this language
                    // we need to respect fallback settings
                    _logger.Debug($"Null returned for resource key `{query.Key}` for language: `{query.Language.Name} Executing InvariantCulture fallback.");

                    return _originalHandler.Execute(new GetTranslation.Query(query.Key, CultureInfo.InvariantCulture, false));
                }

                return foundTranslation;
            }
        }

        public class HandlerWithLogging : IQueryHandler<GetTranslation.Query, string>
        {
            private readonly GetTranslationHandler _inner;
            private readonly ILogger _logger;

            public HandlerWithLogging(GetTranslationHandler inner)
            {
                _inner = inner;
                _logger = LogManager.GetLogger(typeof(Handler));
            }

            public string Execute(GetTranslation.Query query)
            {
                var result = _inner.Execute(query);
                if(result == null)
                    _logger.Warning($"MISSING: Resource Key (culture: {query.Language.Name}): {query.Key}. Probably class is not decorated with either [LocalizedModel] or [LocalizedResource] attribute.");

                return result;
            }
        }
    }
}
