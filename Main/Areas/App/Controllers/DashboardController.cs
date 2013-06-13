using System.Web.Mvc;
using SDM.Main.Helpers.Controllers;

namespace SDM.Main.Areas.App.Controllers
{    
    public class DashboardController : CommonControllerBase
    {
        public ActionResult Index()
        {            
            return View();
        }
    }
}
