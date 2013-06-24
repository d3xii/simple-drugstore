using System;
using System.Collections.Generic;
using MvcControls.CustomHtmlHelper.Button;
using MvcControls.CustomHtmlHelper.DataGrid;

namespace MvcControls.CustomHtmlHelper
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
        public HtmlString Render(Func<T, IHtmlControl> selector)
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
        public HtmlString Render(IHtmlControl control)
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
        public DataGridControl<T, TElement> Grid<TElement>(Func<T, ICollection<TElement>> dataSourceSelector, string width = null)
        {
            return new DataGridControl<T, TElement>(this._helper, width).DataSource(dataSourceSelector(this._helper.ViewData.Model));
        }

        /// <summary>
        /// Renders a button.
        /// </summary>
        public ButtonControl<T> Button(string displayText)
        {
            return new ButtonControl<T>(_helper, displayText);
        }

        #endregion

        
    }
}