﻿using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using MvcControls.Controls.Base;

namespace MvcControls.Controls.TextBox
{
    /// <summary>
    /// Provides methods to render a datagrid.
    /// </summary>
    public class TextBoxControl<TModel> : HtmlControlBase<TModel, TextBoxRenderInfo>
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
        public TextBoxControl<TModel> Bind<TResult>(Expression<Func<TModel, TResult>> propertySelector)
        {
            this.RenderInfo.PropertyName = ExpressionHelper.GetExpressionText(propertySelector);
            this.RenderInfo.PropertyValue = !ReferenceEquals(this.Model, null) ? (object) propertySelector.Compile()(this.Model) : default(TResult);
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