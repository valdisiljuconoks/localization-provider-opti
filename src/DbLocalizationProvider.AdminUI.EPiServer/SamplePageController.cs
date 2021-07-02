using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DbLocalizationProvider.AdminUI.EPiServer
{
    public class UiHostPageController : Controller
    {
        [Authorize(Roles = "CmsAdmins")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
