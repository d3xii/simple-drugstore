﻿using System.Web.Mvc;

namespace SDM.Main.Areas.Admin
{
    /// <summary>
    /// Register area: "/admin".
    /// </summary>
    public class AdminAreaRegistration : AreaRegistration
    {
        /// <summary>
        /// Gets the name of the area to register.
        /// </summary>
        /// <returns>
        /// The name of the area to register.
        /// </returns>
        public override string AreaName
        {
            get { return "Admin"; }
        }

        /// <summary>
        /// Registers an area in an ASP.NET MVC application using the specified area's context information.
        /// </summary>
        /// <param name="context">Encapsulates the information that is required in order to register the area.</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("Admin_default", "Admin/{controller}/{action}/{id}", new {controller = "AdminHome", action = "Index", id = UrlParameter.Optional});
        }
    }
}
