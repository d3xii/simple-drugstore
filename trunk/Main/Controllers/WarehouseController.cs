using System.Collections.Generic;
using System.Web.Mvc;
using SDM.Main.Models;

namespace SDM.Main.Controllers
{
    public class WarehouseController : Controller
    {
        public ActionResult Items()
        {
            // create some items
            List<TempItem> items = new List<TempItem>
                {
                    new TempItem {ID = 0, Name = "123"},
                    new TempItem {ID = 0, Name = "abc"}
                };

            return View(items);
        }
    }
}
