using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SDM.Domain.Models.Common;
using SDM.Localization.Core;
using SDM.Main.Helpers.Attributes;
using SDM.Main.Helpers.Extensions;
using SDM.Main.Views.Home;
using ControllerContext = SDM.Main.Helpers.Extensions.ControllerContext;

namespace SDM.Main.Controllers
{
    [CustomErrorHandle, CustomAuthorize]
    public class HomeController : Controller, ILocalizable<HomeControllerTexts>
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            // if authenticated
            if (this.HttpContext.User.Identity.IsAuthenticated)
            {
                // redirect to dashboard
                return this.RedirectToAction("Index", "Dashboard");
            }

            return View("HomeIndex");
        }
        
        [AllowAnonymous]
        public ActionResult Login()
        {
            // redirect to home page
            return this.RedirectToAction("Index");
        }

        [HttpPost, AllowAnonymous]
        public ActionResult Login(HomeIndexViewModel viewModel)
        {
            // validate model
            if (!ModelState.IsValid)
            {
                return this.View("HomeIndex", viewModel);
            }

            // prepare service
            using (ControllerContext context = this.GetContext())
            {
                // get user name
                AccountModel accountModel = context.AccountRepository.GetByNameAndPassword(viewModel.UserName, viewModel.Password);

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
                return this.View("HomeIndex", viewModel);
            }
        }

        public ActionResult Logout()
        {
            // logout
            FormsAuthentication.SignOut();

            // redirect to home
            return this.RedirectToAction("Index");
        }
    }
}
