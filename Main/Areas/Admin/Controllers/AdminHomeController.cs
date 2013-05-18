using System.Web.Mvc;
using SDM.Core.Configuration;
using SDM.Core.Context;
using SDM.Localization.Core;

namespace SDM.Main.Areas.Admin.Controllers
{
    /// <summary>
    /// Represents the Home.
    /// </summary>
    [CustomAuthorize]
    public class AdminHomeController : Controller, ILocalizable<AdminHomeControllerTexts>
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
        public string Login(string returningUrl, string password)
        {
            // read from config
            ConfigManager configManager = new ConfigManager();
            Config config = configManager.Load(new ServerContext(this.Server));

            // check same password
            if (password == config.AdminPassword)
            {
                // ok
                return "Login OK.";
            }
            else
            {
                // invalid password
                return this.Localize(t => t.InvalidPassword);
            }

            return null;
        }
    }
}