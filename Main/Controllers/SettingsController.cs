using System.Web.Mvc;
using SDM.Localization.Core;
using SDM.Main.Helpers.Attributes;
using SDM.Main.Helpers.Extensions;
using SDM.Main.Helpers.Extensions.CustomHtmlHelper;
using SDM.Main.Views.Settings;
using ControllerContext = SDM.Main.Helpers.Extensions.ControllerContext;

namespace SDM.Main.Controllers
{
    [CustomErrorHandle, CustomAuthorize]
    public class SettingsController : Controller, ILocalizable<SettingsController.Texts>
    {
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public string TestRemove()
        {
            this.HttpContext.Cache.Remove("test");
            return null;
        }

        public ActionResult ChangePassword(ChangePasswordViewModel viewModel)
        {
            // validate
            if (!ModelState.IsValid)
            {
                return this.View("Index");
            }

            using (ControllerContext context = this.GetContext())
            {
                // try to change account password
                string error = context.CurrentAccount.ChangePassword(viewModel.CurrentPassword, viewModel.NewPassword, viewModel.NewPassword2);

                // if any error
                if (error != null)
                {
                    viewModel.Message = new ErrorHtmlMessage(error);
                }
                else
                {
                    // save to database
                    context.Database.SaveChanges();

                    // no error                                         
                    viewModel.Message = new SuccessHtmlMessage(this.Localize(t => t.PasswordChangedSuccessfully));                    
                }
            }

            // return current view
            return this.View("Index", viewModel);

            //// get account from database
            //using (DatabaseContext context = this.GetContext())
            //{
            //    // find account
            //    AccountRepository accountRepository = new AccountRepository(context);
            //    AccountModel model = accountRepository.GetByName(this.HttpContext.User.Identity.Name);

            //}                       
        }


        //**************************************************
        //
        // Nestsed classes
        //
        //**************************************************

        #region Nestsed classes

        public class Texts : LocalizationScopeBase
        {
            public string PasswordChangedSuccessfully = "Password has been changed successfully.";
        }

        #endregion

    }
}