using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using SDM.ApplicationServices.Configuration;
using SDM.Infrastructure.Database;
using SDM.Infrastructure.Hdd;
using SDM.Localization.Core;
using SDM.Main.Helpers.Attributes;

namespace SDM.Main.Areas.Admin.Controllers
{
    /// <summary>
    /// Represents the Admin Home.
    /// </summary>
    [CustomAuthorize, CustomErrorHandle]
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
            // get unsaved config
            SqlConfigModel unsavedConfig = this.GetUnsavedConfig(sqlConfig);
            string errorMessage = this.ValidateSqlConfig(unsavedConfig);

            // return if there is any error
            if (!string.IsNullOrEmpty(errorMessage))
            {
                return errorMessage;
            }

            // try to create a database connect, if != null ==> success
            string result = new DatabaseContextFactory().TestConnectionString(unsavedConfig);

            // return result
            return result == null ? this.Localize(t => t.ValidDatabaseConnection) : string.Format(this.Localize(t => t.InvalidDatabaseConnection), result);
        }

        //public string FormatDatabase(SqlConfigModel sqlConfig)
        //{
        //    // get unsaved config
        //    SqlConfigModel unsavedConfig = this.GetUnsavedConfig(sqlConfig);
        //    string errorMessage = this.ValidateSqlConfig(unsavedConfig);

        //    // return if there is any error
        //    if (!string.IsNullOrEmpty(errorMessage))
        //    {
        //        return errorMessage;
        //    }

        //    // try to create a database connect, if != null ==> success
        //    using (DatabaseContext databaseContext = new DatabaseContextFactory().CreateContext(unsavedConfig))
        //    {
        //        if (databaseContext == null)
        //        {
        //            return this.Localize(t => t.InvalidDatabaseConnection);
        //        }

        //        // redirect to console
        //        return "REDIRECT";
        //    }
        //}

        [HttpPost]
        public void FormatDatabase(string dummy)
        {
            // get saved config
            SqlConfigModel config = this.ReadConfig().Sql;

            // try to create a database connect, if != null ==> success
            DatabaseContext databaseContext = new DatabaseContextFactory().CreateContext(config);

            // start service
            var service = new DatabaseFormatter(databaseContext)
                              {
                                  Tag = databaseContext
                              };

            // add to session
            this.Session[typeof(DatabaseFormatter).Name] = service;

            // start session
            service.Start();
            databaseContext.SaveChanges();
        }

        [HttpGet]
        public ActionResult FormatDatabase()
        {
            // get from session
            DatabaseFormatter service = (DatabaseFormatter)this.Session[typeof(DatabaseFormatter).Name];

            // not started
            if (service == null)
            {
                // null
                return null;
            }

            // try to get messages
            string[] messages = service.GetAndClearPendingMessages();

            // if has data
            if (messages.Length > 0)
            {
                // return result
                return new ContentResult
                           {
                               Content = string.Join(Environment.NewLine, messages)
                           };
            }

            // ended?
            if (service.IsStopped)
            {
                // clear session
                this.Session.Remove(typeof(DatabaseFormatter).Name);

                // get back tag
                DatabaseContext context = (DatabaseContext)service.Tag;

                // save if success
                if (service.IsSuccess)
                {
                    context.SaveChanges();
                }

                // dispose it
                context.Dispose();
                return null;
            }

            return new HttpStatusCodeResult(HttpStatusCode.Continue);
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

        /// <summary>
        /// Gets in-memory config. If the password wasnt filled, it will be taken from saved config.
        /// </summary>
        private SqlConfigModel GetUnsavedConfig(SqlConfigModel configModel)
        {
            // if the password is defined            
            if (configModel.IsPasswordDefined())
            {
                // just take it
                return configModel;
            }

            // read config
            ConfigModel systemConfig = this.ReadConfig();

            // clone it
            return new SqlConfigModel
                       {
                           DatabaseName = configModel.DatabaseName,
                           Password = systemConfig.Sql.Password ?? string.Empty,
                           ServerName = configModel.ServerName,
                           UserName = configModel.UserName
                       };
        }

        /// <summary>
        /// Validates SQL config and returns error messages, if any.
        /// </summary>
        private string ValidateSqlConfig(SqlConfigModel sqlConfig)
        {
            // try to validate it
            this.ModelState.Clear();
            this.TryValidateModel(sqlConfig);

            // validate first
            if (!ModelState.IsValid)
            {
                return string.Format(this.Localize(t => t.InvalidDatabaseConnection),
                                     string.Join("\r", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
            }

            // no error
            return null;
        }

        #endregion
    }
}