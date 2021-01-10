# Working with Resources (EPiServer)

By default translations in `CultureInfo.CurrentUICulture` will be returned.

Following resource used in samples:

```csharp
namespace MySampleProject {

    [LocalizedResource]
    public class MyResources
    {
        public static string SampleResource => "This is default value";
    }
}
```

## Translating in Markup (.cshtml)

Now, you may use one of the ways to output this resource to the end-users:

```
@using DbLocalizationProvider

<div>
    @Html.Translate(() => MySampleProject.MyResources.SampleResource)
</div>
```

Retrieve translation by specific culture ("Norsk" in this case):

```
@using DbLocalizationProvider

<div>
    @Html.TranslateByCulture(() => MySampleProject.MyResources.SampleResource,
                             CultureInfo.GetCultureInfo("no"))
</div>
```


## Translating in C#

It's also possible to retrieve translation in C# if needed (for example when localizing messages in services).

```csharp
using DbLocalizationProvider;
using EPiServer.Framework.Localization;

...

var t = LocalizationService.Current.GetString(() => MySampleProject.MyResources.SampleResource);
```

Retrieve translation by specific culture ("Norsk" in this case):

```csharp
using DbLocalizationProvider;
using EPiServer.Framework.Localization;

...

var t2 = LocalizationService.Current.GetStringByCulture(
    () => MySampleProject.MyResources.SampleResource,
    CultureInfo.GetCultureInfo("no"));
```

## EPiServer Page Property of Type System.Enum

If you want to have content type property of `System.Enum` and at the same time have it localized, then you should decorate your property with `[LocalizedEnum]` attribute:

```csharp
[ContentType(...
[LocalizedModel(OnlyIncluded = true)]
public class StartPage : PageData
{
    [LocalizedEnum(typeof(SomeValuesEnum))]
    [BackingType(typeof(PropertyNumber))]
    public virtual SomeValuesEnum SomeValue { get; set; }
}
```
Enum type itself should have expected attributes when working with [basic Enum translations](https://github.com/valdisiljuconoks/LocalizationProvider/blob/master/docs/translate-enum-net.md):

```csharp
[LocalizedResource]
public enum SomeValuesEnum
{
    [Display(Name = "NOONE!")]
    None = 0,

    [Display(Name = "1st value")]
    FirstValue = 1,

    [Display(Name = "This is second")]
    SecondValue = 2,

    [Display(Name = "And here comes last (3rd)")]
    ThirdOne = 3
}
```

`[LocalizedEnum]` attribute will make sure to tell EPiServer to setup things accordingly and assign specific localized selection factory type to provide correctly translated enum values.

## Translating EPiServer Categories

If you need to translate categories in EPiServer you need to decorate category definition with `[LocalizedCategory]`.

For example:

```csharp
[LocalizedCategory]
public class SampleCategory : Category
{
    public SampleCategory()
    {
        Name = "This is sample cat. from code";
    }
}
```

`Name` value will be assigned to invariant culture by default.

Resource key assigned to the localized category is `/categories/category[@name=\"{CategoryName}\"]/description`.
