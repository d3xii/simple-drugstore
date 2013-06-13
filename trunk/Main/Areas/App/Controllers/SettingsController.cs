using System.Linq;
using System.Web.Mvc;
using SDM.Core.Localization;
using SDM.Localization.Core;
using SDM.Main.Areas.App.Views.Settings;
using SDM.Main.Helpers.Attributes;
using SDM.Main.Helpers.Controllers;
using SDM.Main.Helpers.Extensions.CustomHtmlHelper.Message;

namespace SDM.Main.Areas.App.Controllers
{
    [CustomErrorHandle, CustomAuthorize]
    public class SettingsController : CommonControllerBase, ILocalizable<SettingsController.Texts>
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChangePassword(ChangePasswordViewModel viewModel)
        {
            // validate
            if (!ModelState.IsValid)
            {
                return this.View("Index");
            }

            // try to change account password
            string error = this.Data.CurrentAccount.ChangePassword(viewModel.CurrentPassword, viewModel.NewPassword, viewModel.NewPassword2);

            // if any error
            if (error != null)
            {
                viewModel.Message = new ErrorHtmlMessage(error);
            }
            else
            {
                // save to database
                this.Data.Database.SaveChanges();

                // no error                                         
                viewModel.Message = new SuccessHtmlMessage(this.Localize(t => t.PasswordChangedSuccessfully));
            }

            // return current view
            return this.View("Index", viewModel);
        }

        public ActionResult System()
        {
            // get all accounts
            var models = this.Data.Database.Accounts.ToArray();

            return this.View(models);
        }


        //**************************************************
        //
        // Nestsed classes
        //
        //**************************************************

        #region Nestsed classes

        public class Texts : CustomLocalizationScopeBase
        {
            public string PasswordChangedSuccessfully = "Password has been changed successfully.";
        }

        #endregion
    }
}