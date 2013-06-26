using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using MvcControls.Controls.Base;
using MvcControls.Controls.Button;
using MvcControls.Controls.DataGrid;
using MvcControls.Controls.TextBox;

namespace MvcControls.Controls
{
    /// <summary>
    /// Provides custom helper methods.
    /// </summary>
    public class CustomHeper<T> where T : class
    {
        //**************************************************
        //
        // Private methods
        //
        //**************************************************

        #region Private methods

        /// <summary>
        /// Gets the helper underlying within custom helper.
        /// </summary>
        private readonly HtmlHelper<T> _helper;

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
        public CustomHeper(HtmlHelper<T> helper)
        {
            _helper = helper;
        }

        #endregion


        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods

        /// <summary>
        /// Renders a custom control.
        /// </summary>
        public IHtmlString Render(Func<T, IHtmlControl> selector)
        {
            // skip if the model is NULL
            if (this._helper.ViewData.Model == null)
            {
                return null;
            }

            // render control
            return this.Render(selector(this._helper.ViewData.Model));
        }

        /// <summary>
        /// Renders a custom control.
        /// </summary>
        public IHtmlString Render(IHtmlControl control)
        {
            // skip if null
            if (control == null)
            {
                return null;
            }

            // render control
            return control.Render(_helper);
        }

        /// <summary>
        /// Renders a datagrid.
        /// If the width is null, "auto" will be used.
        /// </summary>
        public DataGridControl<T, TElement> Grid<TElement>(Expression<Func<T, ICollection<TElement>>> dataSourceSelector)
        {
            // get data source
            string dataSourcePropertyName = ExpressionHelper.GetExpressionText(dataSourceSelector);                
            var collection = dataSourceSelector.Compile()(_helper.ViewData.Model);

            return new DataGridControl<T, TElement>(this._helper, (ICollection) collection, dataSourcePropertyName);
        }

        /// <summary>
        /// Renders a button.
        /// </summary>
        public ButtonControl<T> Button()
        {
            return new ButtonControl<T>(_helper);
        }

        /// <summary>
        /// Renders a text box.
        /// </summary>
        /// <returns></returns>
        public TextBoxControl<T> TextBox()
        {
            return new TextBoxControl<T>(_helper);
        }


        #endregion

        
    }
}