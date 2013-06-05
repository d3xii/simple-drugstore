using System.Web.Mvc;
using System.Web.Security;
using SDM.ApplicationServices.Configuration;
using SDM.Infrastructure.Database;
using SDM.Infrastructure.Hdd;
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
            ConfigModel config = this.ReadConfig();
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
            ConfigModel config = this.ReadConfig();

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
        public ActionResult SaveSettings(ConfigModel config)
        {
            // read config
            ConfigModel systemConfig = this.ReadConfig();

            // prepare config services
            ConfigServices configServices = new ConfigServices();

            // if the admin password is changed, force relogging
            bool isAdminPasswordChanged = configServices.IsAdminPasswordChanged(config, systemConfig);

            // copy submitted values to system config
            configServices.CopyValues(config, systemConfig);

            // save to file
            new ConfigRepository(new FileAccessProvider(this.Server)).Save(systemConfig);

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
            // just create new config instance and save back to data store            
            ConfigModel newConfig = new ConfigFactory().Create();
            new ConfigRepository(new FileAccessProvider(this.Server)).Save(newConfig);

            // save to view bag
            ViewBag.Result = "Settings have been reset to default values.";

            // return current view
            return View("Index", newConfig);
        }

        public string TestDatabaseConnection(SqlConfigModel sqlConfig)
        {
            // try to create a database connect, if != null ==> success
            DatabaseContext databaseContext = new DatabaseContextFactory().CreateContext(sqlConfig);

            // return result
            return databaseContext != null ? this.Localize(t => t.ValidDatabaseConnection) : this.Localize(t => t.InvalidDatabaseConnection);
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
        private ConfigModel ReadConfig()
        {
            // read from config
            ConfigRepository repository = new ConfigRepository(new FileAccessProvider(this.Server));
            return repository.Load();
        }

        #endregion        
    }
}