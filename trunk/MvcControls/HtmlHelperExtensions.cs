using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcControls.Controls;

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

        /// <summary>
        /// Joins the HTML string of 2 controls.
        /// </summary>
        public static IHtmlString Combine(this HtmlHelper helper, params IHtmlString[] values)
        {
            return new HtmlString(string.Join(null, values.Select(t => t.ToHtmlString().Trim())));
        }        
    }
}