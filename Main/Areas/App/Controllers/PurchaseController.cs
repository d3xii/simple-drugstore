using System.Linq;
using System.Web.Mvc;
using SDM.Domain.Models;
using SDM.Main.Helpers.Attributes;
using SDM.Main.Helpers.Controllers;
using SDM.Main.Helpers.Extensions.CustomHtmlHelper.Message;

namespace SDM.Main.Areas.App.Controllers
{
    [CustomErrorHandle, CustomAuthorize]
    public class PurchaseController : CommonControllerBase
    {
        public ActionResult Transactions()
        {
            return View(this.Data.Database.PurchaseTransactions.OrderByDescending(t => t.Time).ToArray());
        }

        public ActionResult AddTransaction()
        {
            return this.View(new PurchaseTransactionModel());
        }

        [HttpPost]
        public ActionResult AddTransaction(PurchaseTransactionModel model)
        {
            // validate
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            // validate to create
            var error = model.Create(this.Data.Database.PurchaseTransactions);
            if (error != null)
            {
                this.ViewBag.ResultMessage = new ErrorHtmlMessage(error);
                return this.View(model);
            }

            // save
            this.Data.Database.SaveChanges();

            // refresh
            return this.RedirectToAction("Transactions");
        }

        public ActionResult EditTransaction(int id)
        {
            // get item
            var model = this.Data.Database.PurchaseTransactions.GetById(id);
            return this.View("AddTransaction", model);
        }

        [HttpPost]
        public ActionResult EditTransaction(PurchaseTransactionModel model)
        {
            // validate
            if (!ModelState.IsValid)
            {
                return this.View("AddTransaction", model);
            }

            // find the existed one the database
            var savedModel = this.Data.Database.PurchaseTransactions.GetByIdOrThrowException(model.ID);

            // validate to update
            var error = savedModel.Update(model);
            if (error != null)
            {
                this.ViewBag.ResultMessage = new ErrorHtmlMessage(error);
                return this.View("AddTransaction", model);
            }

            // add to database
            this.Data.Database.SaveChanges();

            // refresh
            return this.RedirectToAction("Transactions");
        }

        [HttpPost]
        public ActionResult DeleteTransaction(int id)
        {
            // delete given account
            this.Data.Database.PurchaseTransactions.Remove(id);
            this.Data.Database.SaveChanges();

            // refresh
            return this.RedirectToAction("Transactions");
        }
    }
}
