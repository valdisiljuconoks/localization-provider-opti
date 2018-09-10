using System.Globalization;
using System.Web.Mvc;
using DbLocalizationProvider.EPiServer.Sample.Models.Pages;
using DbLocalizationProvider.EPiServer.Sample.Models.ViewModels;
using EPiServer.Framework.Localization;
using EPiServer.Logging;
using EPiServer.Web.Mvc;

namespace DbLocalizationProvider.EPiServer.Sample.Controllers
{
    public class StartPageController : PageController<StartPage>
    {
        private readonly LocalizationService _service;
        private readonly LocalizationProvider _provider;

        public StartPageController(LocalizationService service, LocalizationProvider provider)
        {
            _service = service;
            _provider = provider;
        }

        public ActionResult Index(StartPage currentPage)
        {
            LogManager.GetLogger(typeof(StartPageController)).Log(Level.Information, "Test log message");

            var tr = _provider.GetString("DbLocalizationProvider.EPiServer.Sample.Models.Pages.SomeValuesEnum.SecondValue");
            var trSv = _provider.GetStringByCulture("DbLocalizationProvider.EPiServer.Sample.Models.Pages.SomeValuesEnum.SecondValue", new CultureInfo("sv"));
            var trInv = _provider.GetStringByCulture("DbLocalizationProvider.EPiServer.Sample.Models.Pages.SomeValuesEnum.SecondValue", CultureInfo.InvariantCulture);
            var trLv = _provider.GetStringByCulture("DbLocalizationProvider.EPiServer.Sample.Models.Pages.SomeValuesEnum.SecondValue", new CultureInfo("lv"));

            var svc = _service.GetString("DbLocalizationProvider.EPiServer.Sample.Models.Pages.SomeValuesEnum.SecondValue");
            var svcSv = _service.GetStringByCulture("DbLocalizationProvider.EPiServer.Sample.Models.Pages.SomeValuesEnum.SecondValue", new CultureInfo("sv"));
            var svcInv = _service.GetStringByCulture("DbLocalizationProvider.EPiServer.Sample.Models.Pages.SomeValuesEnum.SecondValue", CultureInfo.InvariantCulture);
            var svcLv = _service.GetStringByCulture("DbLocalizationProvider.EPiServer.Sample.Models.Pages.SomeValuesEnum.SecondValue", new CultureInfo("lv"));

            var enumTr = SomeValuesEnum.FirstValue.Translate(CultureInfo.InvariantCulture);
            var enumSv = SomeValuesEnum.FirstValue.TranslateByCulture(new CultureInfo("sv"));
            var enumInv = SomeValuesEnum.FirstValue.TranslateByCulture(CultureInfo.InvariantCulture);
            var enumLv = SomeValuesEnum.FirstValue.TranslateByCulture(new CultureInfo("lv"));

            return View(new StartPageViewModel(currentPage));
        }
    }
}
