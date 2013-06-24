using System.Web;
using System.Web.Mvc;

namespace MvcControls.Controls.Base
{
    /// <summary>
    /// Defines a flexible HTML control.
    /// </summary>
    public interface IHtmlControl
    {
        /// <summary>
        /// Renders this html message to MVC stream.
        /// </summary>
        IHtmlString Render(HtmlHelper helper);        
    }
}