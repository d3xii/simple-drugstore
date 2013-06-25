using System.Web.Mvc;
using MvcControls.Controls;
using MvcControls.Controls.Parameters;

namespace MvcControls
{
    /// <summary>
    /// Provides entry point to custom HTML helper.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Uses custom-built helpers.
        /// </summary>
        public static CustomHeper<T> Custom<T>(this HtmlHelper<T> helper) where T : class
        {
            return new CustomHeper<T>(helper);
        }
    }

    /// <summary>
    /// Provides entry point to custom URL helper.
    /// </summary>
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Renders the URL to the action.
        /// </summary>
        public static string Action(this UrlHelper helper, ActionParameter action)
        {
            return helper.Action(action.ActionName, action.ControllerName, action.RouteValues);
        }
    }
}