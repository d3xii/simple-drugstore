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
            Config config = this.ReadConfig();
            return View(config);
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
            Config config = this.ReadConfig();

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
        public ActionResult SaveSettings(Config config)
        {
            // read config
            Config systemConfig = this.ReadConfig();

            // if the admin password is changed, force relogging
            bool isAdminPasswordChanged = config.AdminPassword != systemConfig.AdminPassword;

            // copy submitted values to system config
            ConfigManager configManager = new ConfigManager();
            config.CopyValues(systemConfig);            

            // save to file
            configManager.Save(new ServerContext(this.Server), systemConfig);

            // if the admin password is changed, force relogging
            if (isAdminPasswordChanged)
            {
                // clear session
                FormsAuthentication.SignOut();
                return RedirectToAction("Index");
            }

            // save to view bag
            ViewBag.Result = "Settings have been saved.";

            // return Index view
            return View("Index", config);
        }

        public ActionResult ResetSettings()
        {
            // reset config
            ConfigManager configManager = new ConfigManager();
            Config config = new Config().ResetValues();
            configManager.Save(new ServerContext(this.Server), config);

            // save to view bag
            ViewBag.Result = "Settings have been reset to default values.";

            // return current view
            return View("Index", config);
        }

        //**************************************************
        //
        // Private methods
        //
        //**************************************************

        #region Private methods

        /// <summary>
        /// Reads server config.
        /// </summary>
        private Config ReadConfig()
        {
            // read from config
            ConfigManager configManager = new ConfigManager();
            return configManager.Load(new ServerContext(this.Server));         
        }

        #endregion

    }
}