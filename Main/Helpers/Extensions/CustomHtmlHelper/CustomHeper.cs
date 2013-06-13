using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using SDM.Main.Helpers.Extensions.CustomHtmlHelper.DataGrid;

namespace SDM.Main.Helpers.Extensions.CustomHtmlHelper
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
            return selector(this._helper.ViewData.Model).Render(_helper);
        }

        /// <summary>
        /// Renders a datagrid.
        /// </summary>
        public DataGridControl<T, TElement> Grid<TElement>(Func<T, ICollection<TElement>> dataSourceSelector)
        {
            return new DataGridControl<T, TElement>(this._helper).DataSource(dataSourceSelector(this._helper.ViewData.Model));
        }

        #endregion

        
    }
}