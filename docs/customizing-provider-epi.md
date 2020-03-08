# Customizing Localization Provider

## Setup & Configuration
Adjusting various toggles and knobs for the localization provider can be done via configuration context (`ConfigurationContext`).
Episerver initialization module is the best place to do it:

```csharp
using DbLocalizationProvider;

[InitializableModule]
public class InitializationModule1 : IInitializableModule
{
    public void Initialize(InitializationEngine context)
    {
        ConfigurationContext.Setup(ctx => ... );
    }

    public void Uninitialize(InitializationEngine context) { }
}
```

## Fallback Languages
LocalizationProvider gives you option to configure fallback languages for translation lookup.
It means that provider will try to get translation in requested language. And if it does not exist in that language, fallback language list is used to decide which language to try next until either succeeds or fails with no translation found.

By default fallback language is read from Episerver configuration file (section `/episerver.framework/localization`).

If we look at following example configuration file section:

```xml
<episerver.framework>
    <localization fallbackBehavior="FallbackCulture" fallbackCulture="en">
        <providers>
        <add name="db" type="DbLocalizationProvider.EPiServer.DatabaseLocalizationProvider, DbLocalizationProvider.EPiServer" />

        <add name="languageFiles" virtualPath="~/Resources/LanguageFiles" type="EPiServer.Framework.Localization.XmlResources.FileXmlLocalizationProvider, EPiServer.Framework.AspNet" />
        </providers>
    </localization>
</episerver.framework>
```

This fragment will following effect on localization provider:

* Fallback language is added to `FallbackCultures` only if behavior contains `FallbackCulture` flag;
* "English" (`"en"`) language will be used as default fallback language;

But you can also configure and override this from the code.
To configure fallback languages use code fragment below:

```csharp
using DbLocalizationProvider;

[InitializableModule]
public class InitializationModule1 : IInitializableModule
{
    public void Initialize(InitializationEngine context)
    {
        ConfigurationContext.Setup(_ =>
        {
            ...

            // try "sv" -> "no" -> "en"
            _.FallbackCultures
                .Try(new CultureInfo("sv"))
                .Then(new CultureInfo("no"))
                .Then(new CultureInfo("en"));

            _.EnableInvariantCultureFallback = true;
        });
    }

    public void Uninitialize(InitializationEngine context) { }
}
```

This means that following logic will be used during translation lookup:

1) Developer requests translation in Swedish culture (`"sv"`) using `ILocalizationProvider.GetString(() => ...)` method.
2) If translation does not exist -> provider is looking for translation in Norwegian language (`"no"` - second language in the fallback list).
3) If translation is found - one is returned; if not - provider continues process and is looking for translation in English (`"en"`).
4) If there is no translation in English -> depending on `ConfigurationContext.EnableInvariantCultureFallback` setting -> translation in InvariantCulture may be returned.
