﻿using System.Web.Mvc;
using SDM.Main.Helpers.Attributes;

namespace SDM.Main.Controllers
{
    [CustomErrorHandle, CustomAuthorize("Home", "Index")]
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
