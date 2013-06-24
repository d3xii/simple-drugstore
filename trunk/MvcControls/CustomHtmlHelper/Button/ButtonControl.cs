using MvcControls.CustomHtmlHelper.Base;

namespace MvcControls.CustomHtmlHelper.Button
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
        public ButtonControl(HtmlHelper<TModel> helper, string displayText)
            : base(helper)
        {
            RenderInfo.DisplayText = displayText;            
        }

        #endregion


        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods

        /// <summary>
        /// Sets type for this button.
        /// </summary>
        public ButtonControl<TModel> AsType(RenderInfo.ButtonType type)
        {
            RenderInfo.Type = type;
            return this;
        }

        /// <summary>
        /// Marks this button as submit button.
        /// </summary>
        public ButtonControl<TModel> Action([AspMvcAction] string actionName, [AspMvcController]string controllerName = null, object routeValues = null)
        {
            RenderInfo.ActionName = actionName;
            RenderInfo.ControllerName = controllerName;
            RenderInfo.RouteValues = routeValues;
            return this;
        }

        /// <summary>
        /// Sets that the button will show a conformation dialog before performing given action.
        /// </summary>
        public ButtonControl<TModel> HasConfirmation(string text)
        {
            RenderInfo.ConfirmationText = text;
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
            return this.RenderPartial("Button", RenderInfo).ToHtmlString();
        }

        #endregion
    }
}