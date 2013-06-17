using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using JetBrains.Annotations;
using SDM.Main.Helpers.Extensions.CustomHtmlHelper.Base;

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
        public DataGridControl(HtmlHelper<TModel> helper, string width)
            : base(helper)
        {
            _renderInfo.Width = width;
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
        public DataGridControl<TModel, TElement> PropertyColumn(string name, string displayText, Func<TElement, object> valueSelector)
        {
            // add column render info
            _renderInfo.Columns.Add(new RenderInfo.ColumnInfo
                                        {
                                            Name = name,
                                            DisplayText = displayText,
                                            PropertyValueSelector = t => valueSelector((TElement) t),
                                        });
            return this;
        }

        /// <summary>
        /// Adds a dynamic column to the grid.
        /// </summary>
        public DataGridControl<TModel, TElement> DynamicColumn(string name, string displayText, Func<TElement, IHtmlString> htmlRenderer)
        {
            // add column render info
            _renderInfo.Columns.Add(new RenderInfo.ColumnInfo
            {
                Name = name,
                DisplayText = displayText,
                HtmlRenderer = t => htmlRenderer((TElement)t),
            });
            return this;
        }

        /// <summary>
        /// Adds a button to the grid.
        /// </summary>
        public DataGridControl<TModel, TElement> AddButton(string text, [AspMvcAction] string actionName, [AspMvcController] string controllerName = null)
        {
            // add button render info
            _renderInfo.Buttons.Add(new RenderInfo.ButtonInfo
                                        {
                                            DisplayText = text,
                                            ActionName = actionName,
                                            ControllerName = controllerName
                                        });
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
            return this.RenderPartial("DataGrid", this._renderInfo).ToHtmlString();
        }

        #endregion
    }
}