using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace SDM.Main.Helpers.Extensions.CustomHtmlHelper.DataGrid
{
    /// <summary>
    /// Provides methods to render a datagrid.
    /// </summary>
    public class DataGridControl<TModel, TElement> : HtmlControlBase<TModel>
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
        private RenderInfo _renderInfo = new RenderInfo();

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
        public DataGridControl(HtmlHelper<TModel> helper)
            : base(helper)
        {
        }

        #endregion

        #region Overrides of HtmlControlBase<T>

        /// <summary>
        /// Renders this control.
        /// </summary>
        public override HtmlString Render()
        {
            return this.RenderPartial("DataGrid", this._renderInfo);
        }

        #endregion


        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods

        /// <summary>
        /// Sets the datasource of the grid.
        /// </summary>
        public DataGridControl<TModel, TElement> DataSource(ICollection<TElement> dataSource)
        {
            this._renderInfo.DataSource = (ICollection) dataSource;
            return this;
        }

        /// <summary>
        /// Adds a column to the grid.
        /// </summary>
        public DataGridControl<TModel, TElement> Column(string displayName, Func<TElement, object> valueSelector)
        {
            _renderInfo.Columns.Add(new RenderInfo.ColumnInfo
                             {
                                 DisplayName = displayName,
                                 ValueSelector = t => valueSelector((TElement) t),
                                 IsVisible = true
                             });
            return this;
        }

        #endregion
        
    }
}