using System.Web.Mvc;
using MvcControls.Controls;

namespace MvcControls
{
    /// <summary>
    /// Provides entry point to custom helper.
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
}