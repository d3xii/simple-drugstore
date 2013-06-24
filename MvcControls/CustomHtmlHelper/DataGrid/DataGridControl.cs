using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using MvcControls.CustomHtmlHelper.Base;

namespace MvcControls.CustomHtmlHelper.DataGrid
{
    /// <summary>
    /// Provides methods to render a datagrid.
    /// </summary>
    public class DataGridControl<TModel, TElement> : HtmlControlBase<TModel, RenderInfo>
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
        public DataGridControl(HtmlHelper<TModel> helper, string width)
            : base(helper)
        {
            RenderInfo.Width = width;
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
            this.RenderInfo.DataSource = (ICollection)dataSource;
            return this;
        }

        /// <summary>
        /// Adds a column to the grid.
        /// </summary>
        public DataGridControl<TModel, TElement> PropertyColumn(string name, string displayText, Func<TElement, object> valueSelector)
        {
            // add column render info
            RenderInfo.Columns.Add(new RenderInfo.ColumnInfo
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
            RenderInfo.Columns.Add(new RenderInfo.ColumnInfo
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
            RenderInfo.Buttons.Add(new RenderInfo.ButtonInfo
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
            Debug.Assert(this.RenderInfo.DataSource != null);
            return this.RenderPartial("DataGrid", this.RenderInfo).ToHtmlString();
        }

        #endregion
    }
}