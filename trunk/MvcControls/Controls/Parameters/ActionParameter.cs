using System.Web.Mvc;
using JetBrains.Annotations;

namespace MvcControls.Controls.Parameters
{
    /// <summary>
    /// Contains necessary of an action.
    /// </summary>
    public class ActionParameter
    {
        //**************************************************
        //
        // Public variables
        //
        //**************************************************

        #region Public variables

        public string ActionName;
        public string ControllerName;
        public object RouteValues;

        #endregion


        //**************************************************
        //
        // Constructors
        //
        //**************************************************

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ActionParameter([AspMvcAction] string actionName, [AspMvcController] string controllerName = null, object routeValues = null)
        {
            ActionName = actionName;
            ControllerName = controllerName;
            RouteValues = routeValues;
        }

        #endregion    


        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods

        /// <summary>
        /// Renders the URL to the action.
        /// </summary>
        public string ToUrl(UrlHelper helper)
        {
            return helper.Action(this);
        }

        #endregion

    }
}
