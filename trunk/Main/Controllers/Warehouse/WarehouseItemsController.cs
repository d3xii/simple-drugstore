using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SDM.Main.Models;

namespace SDM.Main.Controllers.Warehouse
{
    public class WarehouseItemsController : Controller
    {
        private static readonly List<TempItem> Items;

        static WarehouseItemsController()
        {
            // create some items
            Items = new List<TempItem>
                {
                    new TempItem {ID = 0, Name = "123"},
                    new TempItem {ID = 1, Name = "abc"}
                };

        }

        public ActionResult Index(int? id)
        {
            ViewBag.HighlightId = id;

            return View(Items);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            // if the id is not defined
            if (id == null)
            {
                // show Add form
                return View(new TempItem{ ID = int.MinValue, Name = ""});
            }

            // else: edit item, find item
            TempItem item = Items.SingleOrDefault(t => t.ID == id);

            // not found?
            if (item == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError,
                                                string.Format("Item {0} is not found.", id));
            }

            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            // find item
            TempItem item = id == int.MinValue ? new TempItem() : Items.SingleOrDefault(t => t.ID == id);

            // not found?
            if (item == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError,
                                                string.Format("Item {0} is not found.", id));
            }

            // update item
            item.Name = form["Name"];

            // add needed
            if (id == int.MinValue)
            {
                // get new id
                item.ID = Items.Last().ID + 1;
                Items.Add(item);
            }

            return RedirectToAction("Index", new {id});
        }

        public ActionResult Delete(int id)
        {
            // find item
            TempItem item = Items.SingleOrDefault(t => t.ID == id);

            // not found?
            if (item == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError,
                                                string.Format("Item {0} is not found.", id));
            }

            // delete it
            Items.Remove(item);

            return RedirectToAction("Index");
        }
    }
}
