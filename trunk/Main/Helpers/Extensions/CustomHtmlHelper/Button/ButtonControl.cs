using System.Web.Mvc;
using JetBrains.Annotations;
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
        public ButtonControl(HtmlHelper<TModel> helper, string displayText)
            : base(helper)
        {
            _renderInfo.DisplayText = displayText;            
        }

        #endregion


        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods

        /// <summary>
        /// Marks this button as submit button.
        /// </summary>
        public ButtonControl<TModel> AsSubmitButton()
        {
            _renderInfo.IsSubmitButton = true;
            return this;
        }

        /// <summary>
        /// Marks this button as submit button.
        /// </summary>
        public ButtonControl<TModel> Action([AspMvcAction] string actionName, [AspMvcController]string controllerName = null, object routeValues = null)
        {
            _renderInfo.ActionName = actionName;
            _renderInfo.ControllerName = controllerName;
            _renderInfo.RouteValues = routeValues;
            return this;
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
            return this.RenderPartial("Button", _renderInfo).ToHtmlString();
        }

        #endregion
    }
}