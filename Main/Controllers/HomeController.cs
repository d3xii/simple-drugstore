using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SDM.ApplicationServices.Configuration;
using SDM.Domain.Models.Common;
using SDM.Infrastructure.Database;
using SDM.Infrastructure.Database.Repositories;
using SDM.Infrastructure.Hdd;
using SDM.Localization.Core;
using SDM.Main.Helpers.Attributes;
using SDM.Main.Views.Home;

namespace SDM.Main.Controllers
{
    [CustomErrorHandle, CustomAuthorize("Index")]
    public class HomeController : Controller, ILocalizable<HomeControllerTexts>
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            // if authenticated
            if (this.HttpContext.User.Identity.IsAuthenticated)
            {
                // redirect to dashboard
                return this.RedirectToAction("Dashboard");
            }

            return View("HomeIndex");
        }

        [HttpPost]
        public ActionResult Login(HomeIndexViewModel viewModel)
        {
            // validate model
            if (!ModelState.IsValid)
            {
                return this.View("HomeIndex", viewModel);
            }

            // prepare service
            AccountRepository accountRepository = new AccountRepository(this.CreateContext());

            // get user name
            AccountModel accountModel = accountRepository.GetByNameAndPassword(viewModel.UserName, viewModel.Password);

            // found?
            if (accountModel != null)
            {
                // is enabled?
                if (accountModel.IsEnabled)
                {
                    // save to session
                    var ticket = new FormsAuthenticationTicket(accountModel.UserName, viewModel.IsRememberMe, (int)TimeSpan.FromDays(7).TotalMinutes);

                    // encrypt the ticket and add it to a cookie
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                    Response.Cookies.Add(cookie);

                    // redirect to Dashboard
                    return this.RedirectToAction("Dashboard");
                }

                // not enabled
                viewModel.LoginErrorMessage = this.Localize(t => t.DeactivatedAccount);
            }
            else
            {
                // not found
                viewModel.LoginErrorMessage = this.Localize(t => t.InvalidUserNameOrPassword);
            }

            // stay at current page
            return this.View("HomeIndex", viewModel);
        }

        public ActionResult Dashboard()
        {
            return this.View();
        }


        //**************************************************
        //
        // Private methods
        //
        //**************************************************

        #region Private methods

        /// <summary>
        /// Creates context used by this controller.
        /// </summary>
        private DatabaseContext CreateContext()
        {
            // read from config
            ConfigRepository repository = new ConfigRepository(new FileAccessProvider(this.Server));
            SqlConfigModel sqlConfigModel = repository.Load().Sql;

            // create context
            return new DatabaseContextFactory().CreateContext(sqlConfigModel);
        }

        #endregion

    }
}
