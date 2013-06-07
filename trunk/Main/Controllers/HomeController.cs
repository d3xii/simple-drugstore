using System.Web.Mvc;
using SDM.Localization.Core;
using SDM.Main.Views.Home;

namespace SDM.Main.Controllers
{
    public class HomeController : Controller, ILocalizable<HomeControllerTexts>
    {
        public ActionResult Index()
        {
            return View("HomeIndex");
        }     

        public ActionResult Login(string userName, string password)
        {
            // just show invalid password   
            return this.View("HomeIndex", new HomeIndexViewModel { LoginErrorMessage = this.Localize(t => t.InvalidUserNameOrPassword) });
        }
    }
}
