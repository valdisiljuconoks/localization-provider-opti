using DbLocalizationProvider.Abstractions;

namespace AlloySampleSite.Resources
{
    [LocalizedResource]
    public class PageHeadings
    {
        public static string StartPageHeadingTitle { get; set; } = "Start page heading";
    }

    [LocalizedResource]
    public class SampleResources
    {
        public static string Resource1 { get; set; }
        public static string Resource2 { get; set; }
        public static string Resource3 { get; set; }
        public string InstanceResource4 { get; set; } = "Instance Resource 4";
    }
}
