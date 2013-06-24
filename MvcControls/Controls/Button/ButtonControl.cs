using System.Web.Mvc;
using MvcControls.Controls.Base;

namespace MvcControls.Controls.Button
{
    /// <summary>
    /// Provides methods to render a datagrid.
    /// </summary>
    public class ButtonControl<TModel> : HtmlControlBase<TModel, RenderInfo>
    {
        //**************************************************
        //
        // Constructors
        //
        //**************************************************

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ButtonControl(HtmlHelper<TModel> helper)
            : base(helper)
        {
        }

        #endregion


        #region Overrides of HtmlControlBase<TModel>

        /// <summary>
        /// Returns an HTML-encoded string.
        /// </summary>
        /// <returns>
        /// An HTML-encoded string.
        /// </returns>
        public override string ToHtmlString()
        {
            return this.RenderPartial(string.Format("Button.{0}", this.RenderInfo.Type), RenderInfo).ToHtmlString();
        }

        #endregion
    }
}