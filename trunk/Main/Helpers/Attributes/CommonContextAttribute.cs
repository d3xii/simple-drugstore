using System;
using System.Web.Mvc;

namespace SDM.Main.Helpers.Attributes
{
    /// <summary>
    /// Provdes frequently-used context of an action.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple= false)]
    public class CommonContextAttribute : ActionFilterAttribute
    {
        #region Overrides of ActionFilterAttribute

        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // initialize view model
            filterContext.Controller.ViewBag.IsAdmin = true;
            filterContext.Controller.ViewBag.UserName = "adwdwd";
        }

        #endregion
    }
}