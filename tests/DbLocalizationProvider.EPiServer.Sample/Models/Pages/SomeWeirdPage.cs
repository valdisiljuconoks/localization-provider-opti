using DbLocalizationProvider.Abstractions;
using EPiServer.Core;

namespace DbLocalizationProvider.EPiServer.Sample.Models.Pages
{
    [LocalizedResource(Inherited = false)]
    public class SomeWeirdPage : PageData
    {
        [Ignore]
        public ContentArea ContentArea { get; set; }

        [Include]
        public virtual string SomeProperty { get; set; }
    }
}
