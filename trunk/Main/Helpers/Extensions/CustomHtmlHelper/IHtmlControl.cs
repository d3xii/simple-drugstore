using System.Web;
using System.Web.Mvc;

namespace SDM.Main.Helpers.Extensions.CustomHtmlHelper
{
    /// <summary>
    /// Defines a flexible HTML control.
    /// </summary>
    public interface IHtmlControl
    {
        /// <summary>
        /// Renders this html message to MVC stream.
        /// </summary>
        HtmlString Render(HtmlHelper helper);
    }
}