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

            public Handler()
            {
                _originalHandler = new GetTranslationHandler();
            }

            public string Execute(GetTranslation.Query query)
            {
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
                   && (Equals(query.Language, service.FallbackCulture)) || Equals(query.Language.Parent, CultureInfo.InvariantCulture))
                {
                    // no translation found for this language
                    // we need to respect fallback settings
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
