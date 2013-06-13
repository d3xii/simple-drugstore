using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Security;
using SDM.Core.Localization;
using SDM.Domain.Models;
using SDM.Localization.Core;
using SDM.Main.Helpers.Extensions;

namespace SDM.Main.Areas.App.Controllers
{
    public class HomeController : Controller, ILocalizable<HomeController.Texts>
    {
        public ActionResult Index()
        {
            // if authenticated
            if (this.HttpContext.User.Identity.IsAuthenticated)
            {
                // redirect to dashboard
                return this.RedirectToAction("Index", "Dashboard");
            }

            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel viewModel)
        {
            // validate model
            if (!ModelState.IsValid)
            {
                return this.View("Login", viewModel);
            }

            // prepare service
            using (var context = this.GetContext())
            {
                // get user name
                AccountModel accountModel = context.Database.Accounts.GetByNameAndPassword(viewModel.UserName, viewModel.Password);

                // found?
                if (accountModel != null)
                {
                    // is enabled?
                    if (accountModel.IsEnabled)
                    {
                        //// save to 
                        //var ticket = new FormsAuthenticationTicket(accountModel.UserName, viewModel.IsRememberMe, (int)TimeSpan.FromDays(7).TotalMinutes);

                        //// encrypt the ticket and add it to a cookie
                        //HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                        //Response.Cookies.Add(cookie);
                        FormsAuthentication.SetAuthCookie(accountModel.UserName, viewModel.IsRememberMe, this.Url.Action(string.Empty));

                        // redirect to Dashboard
                        return this.RedirectToAction("Index", "Dashboard");
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
                return this.View("Login", viewModel);
            }
        }

        public ActionResult LogOut()
        {
            // remove cookie            
            FormsAuthentication.SignOut();
            // ReSharper disable PossibleNullReferenceException
            this.Response.Cookies[FormsAuthentication.FormsCookieName].Path = this.Url.Action(string.Empty);
            // ReSharper restore PossibleNullReferenceException

            // redirect to home
            return this.RedirectToAction("Index");
        }


        //**************************************************
        //
        // Nested classes
        //
        //**************************************************

        #region Nested classes

        public class LoginViewModel
        {
            [Required]
            public string UserName { get; set; }

            [Required]
            public string Password { get; set; }

            public bool IsRememberMe { get; set; }

            public string LoginErrorMessage;
        }

        public class Texts : CustomLocalizationScopeBase
        {
            public string InvalidUserNameOrPassword = "Invalid user name or password.";
            public string DeactivatedAccount = "Your account has been deactivated by admin.";
        }

        #endregion

    }
}
