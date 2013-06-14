using System.Web;
using System.Web.Mvc;
using SDM.Main.Helpers.Extensions.CustomHtmlHelper.Base;

namespace SDM.Main.Helpers.Extensions.CustomHtmlHelper.Button
{
    /// <summary>
    /// Provides methods to render a datagrid.
    /// </summary>
    public class ButtonControl<TModel> : HtmlControlBase<TModel>
    {
        //**************************************************
        //
        // Private variables
        //
        //**************************************************

        #region Private variables

        /// <summary>
        /// Gets or sets the internal render information that will be built up by using fluent interface.
        /// </summary>
        private readonly RenderInfo _renderInfo = new RenderInfo();

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
        public ButtonControl(HtmlHelper<TModel> helper, string displayText, string actionName, string controllerName = null)
            : base(helper)
        {
            _renderInfo.DisplayText = displayText;
            _renderInfo.ActionName = actionName;
            _renderInfo.ControllerName = controllerName;
        }

        #endregion

        #region Overrides of HtmlControlBase<T>

        /// <summary>
        /// Renders this control.
        /// </summary>
        public override HtmlString Render()
        {
            return this.RenderPartial("Button", _renderInfo);
        }

        #endregion
    }
}