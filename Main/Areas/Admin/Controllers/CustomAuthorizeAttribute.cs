using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace SDM.Main.Areas.Admin.Controllers
{
    /// <summary>
    /// Authorizes a request and redirects to approritated login page.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        //**************************************************
        //
        // Public properties
        //
        //**************************************************

        #region Public properties

        /// <summary>
        /// Gets name of the controller of the login page.
        /// Default value is NULL which will use the same controller of declared class.
        /// </summary>
        public string Controller { get; private set; }

        /// <summary>
        /// Gets name of the action of the login page.
        /// </summary>
        public string Action { get; private set; }

        #endregion

        //**************************************************
        //
        // Constructors
        //
        //**************************************************

        #region Constructors

        /// <summary>
        /// Marks given controller as [Authorize] and redirects Unauthorized Access to Login action of given controller.
        /// </summary>
        public CustomAuthorizeAttribute(string controller, string action)
        {
            Controller = controller;
            Action = action;
        }

        /// <summary>
        /// Marks given controller as [Authorize] and redirects Unauthorized Access to given action of the same controller.
        /// </summary>
        public CustomAuthorizeAttribute(string action) : this(null, action)
        {
        }

        /// <summary>
        /// Marks given controller as [Authorize] and redirects Unauthorized Access to Login action of the same controller.
        /// </summary>
        public CustomAuthorizeAttribute() : this("Login")
        {
        }

        #endregion



        #region Overrides of AuthorizeAttribute

        /// <summary>
        /// Called when a process requests authorization.
        /// </summary>
        /// <param name="filterContext">The filter context, which encapsulates information for using <see cref="T:System.Web.Mvc.AuthorizeAttribute"/>.</param><exception cref="T:System.ArgumentNullException">The <paramref name="filterContext"/> parameter is null.</exception>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // call base method
            base.OnAuthorization(filterContext);

            // OK?
            if (!(filterContext.Result is HttpUnauthorizedResult))
            {
                // ok
                return;
            }

            // prepare parameter
            var routeValues = new RouteValueDictionary
                {
                    {"action", "Login"},
                    {"ReturnUrl", filterContext.HttpContext.Request.RawUrl}
                };

            // if the controller is defined, add it to route values
            if (!string.IsNullOrEmpty(this.Controller))
            {
                routeValues.Add("controller", this.Controller);
            }

            // redirect
            filterContext.Result = new RedirectToRouteResult(routeValues);
        }

        #endregion
    }
}