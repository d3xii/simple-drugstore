using System.Web.Mvc;
using System.Web.Security;
using SDM.Core.Configuration;
using SDM.Core.Context;
using SDM.Localization.Core;

namespace SDM.Main.Areas.Admin.Controllers
{
    /// <summary>
    /// Represents the Admin Home.
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
        public ActionResult Login(string returningUrl, string password)
        {
            // check empty password
            if (string.IsNullOrEmpty(password))
            {
                // invalid password
                ViewBag.Error = this.Localize(t => t.Mandatory);
                return View();
            }

            // read from config
            ConfigManager configManager = new ConfigManager();
            Config config = configManager.Load(new ServerContext(this.Server));            

            // check same password
            if (password != config.AdminPassword)
            {
                // invalid password
                ViewBag.Error = this.Localize(t => t.InvalidPassword);
                return View();
            }
            
            // save to cookie
            FormsAuthentication.SetAuthCookie("admin", false);

            // ok, redirect to homepage
            return this.RedirectToAction("Index", "AdminHome");
        }

        [HttpPost]
        public string SaveSettings(Config config)
        {
            return "Saving password to: " + config.AdminPassword;
        }
    }
}