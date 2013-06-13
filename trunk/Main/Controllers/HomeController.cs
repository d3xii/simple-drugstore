using System.Web.Mvc;

namespace SDM.Main.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.RedirectToAction("Index", "Home", new { area = "App" });
        }
    }
}
