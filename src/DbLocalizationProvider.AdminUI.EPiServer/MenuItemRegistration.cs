using System.Collections.Generic;
using EPiServer.Framework.Localization;
using EPiServer.Security;
using EPiServer.Shell;
using EPiServer.Shell.Navigation;

namespace DbLocalizationProvider.AdminUI.EPiServer
{
    [MenuProvider]
    public class MenuProvider : IMenuProvider
    {
        private readonly LocalizationService _localizationService;
        private readonly IPrincipalAccessor _principalAccessor;

        public MenuProvider(
            LocalizationService localizationService,
            IPrincipalAccessor principalAccessor)
        {
            _localizationService = localizationService;
            _principalAccessor = principalAccessor;
        }

        public IEnumerable<MenuItem> GetMenuItems()
        {
            var url = Paths.ToResource(GetType(), "uihostpage");

            var link = new UrlMenuItem("Localization", MenuPaths.Global + "/cms/uihostpage", url)
            {
                SortIndex = 100, AuthorizationPolicy = "episerver:localizationprovider:adminui"
            };

            return new List<MenuItem> { link };
        }
    }
}
