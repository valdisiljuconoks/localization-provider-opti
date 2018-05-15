using System.Web;
using System.Web.Caching;
using Newtonsoft.Json;

namespace DbLocalizationProvider.EPiServer.JsResourceHandler
{
    public class DbLocalizationResourceListProvider : IResourceListProvider
    {
        private readonly DbLocalizationProvider.AspNet.Json.JsonConverter _converter;

        public DbLocalizationResourceListProvider(DbLocalizationProvider.AspNet.Json.JsonConverter converter)
        {
            _converter = converter;
        }

        public string GetJson(string filename, HttpContext context, string languageName, bool debugMode)
        {
            return JsonConvert.SerializeObject(_converter.GetJson(filename, languageName), debugMode ? Formatting.Indented : Formatting.None);
        }

        public CacheDependency GetCacheDependency()
        {
            return null;
        }
    }
}
