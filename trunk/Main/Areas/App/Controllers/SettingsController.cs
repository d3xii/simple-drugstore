using System.Linq;
using System.Web.Mvc;
using SDM.Core.Localization;
using SDM.Domain.Models;
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

        public ActionResult AddAccount()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult AddAccount(AccountModel model, string password2)
        {
            // validate
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            // validate to create
            var error = model.Create(password2, this.Data.Database.Accounts);
            if (error != null)
            {
                this.ViewBag.ResultMessage = new ErrorHtmlMessage(error);
                return this.View(model);
            }

            // save
            this.Data.Database.SaveChanges();

            return this.RedirectToAction("System");
        }

        public ActionResult EditAccount(int id)
        {
            // get account
            var accountModel = this.Data.Database.Accounts.GetById(id);

            return this.View("AddAccount", accountModel);
        }

        [HttpPost]
        public ActionResult EditAccount(AccountModel model, string password2)
        {
            // validate
            if (!ModelState.IsValid)
            {
                return this.View("AddAccount", model);
            }

            // find the existed one the database
            var accountModel = this.Data.Database.Accounts.GetByIdOrThrowException(model.ID);

            // validate to update
            var error = accountModel.Update(model, password2);
            if (error != null)
            {
                this.ViewBag.ResultMessage = new ErrorHtmlMessage(error);
                return this.View("AddAccount", model);
            }

            // add to database
            this.Data.Database.SaveChanges();

            // refresh
            return this.RedirectToAction("System");
        }

        [HttpPost]
        public ActionResult DeleteAccount(int id)
        {
            // delete given account
            this.Data.Database.Accounts.Remove(id);
            this.Data.Database.SaveChanges();

            // refresh
            return this.RedirectToAction("System");
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