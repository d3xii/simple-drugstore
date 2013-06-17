using System.Linq;
using System.Web.Mvc;
using SDM.Domain.Models;
using SDM.Main.Helpers.Attributes;
using SDM.Main.Helpers.Controllers;
using SDM.Main.Helpers.Extensions.CustomHtmlHelper.Message;

namespace SDM.Main.Areas.App.Controllers
{
    [CustomErrorHandle, CustomAuthorize]
    public class WarehouseController : CommonControllerBase
    {
        public ActionResult Items()
        {
            return View(this.Data.Database.Items.ToArray());
        }
        
        public ActionResult AddItem()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult AddItem(ItemModel model)
        {
            // validate
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            // validate to create
            var error = model.Create(this.Data.Database.Items);
            if (error != null)
            {
                this.ViewBag.ResultMessage = new ErrorHtmlMessage(error);
                return this.View(model);
            }

            // save
            this.Data.Database.SaveChanges();

            // refresh
            return this.RedirectToAction("Items");
        }

        public ActionResult EditItem(int id)
        {
            // get item
            var model = this.Data.Database.Items.GetById(id);
            return this.View("AddItem", model);
        }

        [HttpPost]
        public ActionResult EditItem(ItemModel model)
        {
            // validate
            if (!ModelState.IsValid)
            {
                return this.View("AddItem", model);
            }

            // find the existed one the database
            var savedModel = this.Data.Database.Items.GetByIdOrThrowException(model.ID);

            // validate to update
            var error = savedModel.Update(model);
            if (error != null)
            {
                this.ViewBag.ResultMessage = new ErrorHtmlMessage(error);
                return this.View("AddItem", model);
            }

            // add to database
            this.Data.Database.SaveChanges();

            // refresh
            return this.RedirectToAction("Items");
        }

        [HttpPost]
        public ActionResult DeleteItem(int id)
        {
            // delete given account
            this.Data.Database.Items.Remove(id);
            this.Data.Database.SaveChanges();

            // refresh
            return this.RedirectToAction("Items");
        }
    }
}
