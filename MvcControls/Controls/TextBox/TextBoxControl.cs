using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using MvcControls.Controls.Base;

namespace MvcControls.Controls.TextBox
{
    /// <summary>
    /// Provides methods to render a datagrid.
    /// </summary>
    public class TextBoxControl<TModel> : HtmlControlBase<TModel, TextBoxControlRenderInfo>
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
        public TextBoxControl(HtmlHelper<TModel> helper)
            : base(helper)
        {
        }

        #endregion


        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods

        /// <summary>
        /// Binds property to this text box.
        /// </summary>
        public TextBoxControl<TModel> Bind(Expression<Func<TModel, object>> propertySelector)
        {
            this.RenderInfo.PropertyName = ExpressionHelper.GetExpressionText(propertySelector);
            this.RenderInfo.PropertyValue = propertySelector.Compile()(this.Model);
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
            return this.RenderPartial("TextBox", RenderInfo).ToHtmlString();
        }

        #endregion
    }
}