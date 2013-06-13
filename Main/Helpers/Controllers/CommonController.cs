using System.Web.Mvc;
using SDM.Main.Helpers.Attributes;
using SDM.Main.Helpers.Extensions;

namespace SDM.Main.Helpers.Controllers
{
    /// <summary>
    /// Provides foundation for general-purpose controller.
    /// </summary>
    [CustomErrorHandle, CustomAuthorize("Home", "Index")]
    public abstract class CommonControllerBase : Controller
    {        
        //**************************************************
        //
        // Protected methods
        //
        //**************************************************

        #region Protected methods

        /// <summary>
        /// Gets common view model of this controller.
        /// This view model will automatically populated before every call to the Action.
        /// </summary>
        protected CommonControllerContext Data { get; private set; }

        #endregion


        #region Overrides of Controller

        /// <summary>
        /// Called before the action method is invoked.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // create new model
            Data = this.GetContext();

            // create common view data
            this.ViewBag.UserName = Data.CurrentAccount.UserName;
            this.ViewBag.IsAdmin = Data.CurrentAccount.IsAdmin;
        }

        /// <summary>
        /// Called after the action method is invoked.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // dispose data
            if (this.Data != null)
            {
                this.Data.Dispose();
                this.Data = null;
            }
        }

        #endregion        
    }
}