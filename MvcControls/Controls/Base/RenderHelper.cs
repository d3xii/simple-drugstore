using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MvcControls.Controls.Base
{
    /// <summary>
    /// Provides support methods to render HTML.
    /// </summary>
    internal static class RenderHelper
    {
        /// <summary>
        /// Renders given partial view within the same folder of the control.
        /// </summary>
        public static IHtmlString RenderTemplate(HtmlHelper helper, string name, object model)
        {
            // get absolute path
            // ReSharper disable PossibleNullReferenceException
            //string path = string.Format("~/{0}/{1}.cshtml", this.GetType().Namespace.Replace("SDM.Main.", string.Empty).Replace('.', '/'), name);
            string path = string.Format("~/bin/Templates/{0}.cshtml", name);
            // ReSharper restore PossibleNullReferenceException

            // render
            return helper.Partial(path, model);
        }
    }
}
