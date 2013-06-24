namespace MvcControls.CustomHtmlHelper
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