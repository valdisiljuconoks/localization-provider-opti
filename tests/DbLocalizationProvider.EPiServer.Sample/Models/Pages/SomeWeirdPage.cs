using DbLocalizationProvider.Abstractions;
using EPiServer.Core;

namespace DbLocalizationProvider.EPiServer.Sample.Models.Pages
{
    [LocalizedResource(KeyPrefix = "/contenttypes/aboutuspage/", Inherited = false)]
    public class SomeWeirdPage : PageData
    {
        [Ignore]
        public virtual ContentArea ContentArea { get; set; }

        [Include]
        public virtual string SomeProperty { get; set; }

        [ResourceKey("properties/herotitle/caption", Value = "Hero Title")]
        [ResourceKey("properties/herotitle/help", Value =
            "Set hero title, if nothings is set default one will be used.")]
        public virtual string HeroTitle { get; set; }
    }
}
