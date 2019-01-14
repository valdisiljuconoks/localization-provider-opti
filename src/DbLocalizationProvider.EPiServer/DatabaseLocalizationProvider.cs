using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DbLocalizationProvider.Abstractions;
using DbLocalizationProvider.AspNet.Queries;
using DbLocalizationProvider.EPiServer.Queries;
using DbLocalizationProvider.Queries;
using EPiServer.Framework.Localization;

namespace DbLocalizationProvider.EPiServer
{
    public class DatabaseLocalizationProvider : global::EPiServer.Framework.Localization.LocalizationProvider
    {
        private readonly IQueryHandler<GetTranslation.Query, string> _originalHandler;

        public DatabaseLocalizationProvider()
        {
            _originalHandler = new GetTranslationHandler();
            if(ConfigurationContext.Current.DiagnosticsEnabled)
                _originalHandler = new EPiServerGetTranslation.HandlerWithLogging(new GetTranslationHandler());
        }

        public override IEnumerable<CultureInfo> AvailableLanguages => new AvailableLanguages.Query().Execute();

        public override string GetString(string originalKey, string[] normalizedKey, CultureInfo culture)
        {
            try
            {
                // we need to call handler directly here
                // if we would dispatch query and ask registered handler to execute
                // we would end up in stack-overflow as in EPiServer context
                // the same database localization provider is registered as the query handler
                var foundTranslation = _originalHandler.Execute(new GetTranslation.Query(originalKey, culture, false));

                // this is last chance for Episerver to find translation (asked in translation fallback language)
                // if no match is found and invariant fallback is configured - return invariant culture translation
                if(foundTranslation == null
                   && LocalizationService.Current.FallbackBehavior.HasFlag(FallbackBehaviors.FallbackCulture)
                   && ConfigurationContext.Current.EnableInvariantCultureFallback
                   && (Equals(culture, LocalizationService.Current.FallbackCulture) || Equals(culture.Parent, CultureInfo.InvariantCulture)))
                {
                    return _originalHandler.Execute(new GetTranslation.Query(originalKey, CultureInfo.InvariantCulture, false));
                }

                return foundTranslation;
            }
            catch(KeyNotFoundException)
            {
                // this can be a case when Episerver initialization infrastructure calls localization provider way too early
                // (before any of the setup code in the provider is executed). this happens if you have DisplayChannels in codebase
                return null;
            }
        }

        public override IEnumerable<global::EPiServer.Framework.Localization.ResourceItem> GetAllStrings(string originalKey, string[] normalizedKey, CultureInfo culture)
        {
            var q = new GetAllTranslations.Query(originalKey, culture);

            return q.Execute()
                    .Select(r => new global::EPiServer.Framework.Localization.ResourceItem(r.Key, r.Value, r.SourceCulture));
        }
    }
}
