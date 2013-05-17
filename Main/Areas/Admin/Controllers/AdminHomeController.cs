using System.Web.Mvc;

namespace SDM.Main.Areas.Admin.Controllers
{
    /// <summary>
    /// Represents the Home.
    /// </summary>
    [AdminAuthorize]
    public class AdminHomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        [AllowAnonymous]
        public string Login(string returningUrl)
        {
            return "From " + returningUrl;
        }
    }
}