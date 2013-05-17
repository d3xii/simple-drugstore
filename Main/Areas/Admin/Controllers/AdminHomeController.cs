using System.Web.Mvc;

namespace SDM.Main.Areas.Admin.Controllers
{
    /// <summary>
    /// Represents the Home.
    /// </summary>
    public class AdminHomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
