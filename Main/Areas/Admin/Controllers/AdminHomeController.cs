using System.Web.Mvc;

namespace SDM.Main.Areas.Admin.Controllers
{
    /// <summary>
    /// Represents the Home.
    /// </summary>
    [CustomAuthorize]
    public class AdminHomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        [AllowAnonymous]
        public ActionResult Login(string returningUrl)
        {
            // assign view data
            ViewBag.ReturningUrl = returningUrl;

            return View();
        }

        [AllowAnonymous, HttpPost]
        public ActionResult Login(string returningUrl, string password)
        {
            // read from config

            return View();
        }
    }
}