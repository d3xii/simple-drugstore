using System;
using System.Collections;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using JetBrains.Annotations;
using MvcControls.Controls.Base;

namespace MvcControls.Controls.DataGrid
{
    /// <summary>
    /// Provides methods to render a datagrid.
    /// </summary>
    public class DataGridControl<TModel, TElement> : HtmlControlBase<TModel, DataGridControlRenderInfo>
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
        public DataGridControl(HtmlHelper<TModel> helper, ICollection dataSource)
            : base(helper)
        {
            this.RenderInfo.DataSource = dataSource;
        }

        #endregion


        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods        

        /// <summary>
        /// Adds a column to the grid.
        /// </summary>
        public DataGridControl<TModel, TElement> PropertyColumn(string name, string displayText, Func<TElement, object> valueSelector)
        {
            // add column render info
            RenderInfo.Columns.Add(new DataGridControlRenderInfo.ColumnInfo
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
            RenderInfo.Columns.Add(new DataGridControlRenderInfo.ColumnInfo
            {
                Name = name,
                DisplayText = displayText,
                HtmlRenderer = t => htmlRenderer((TElement)t),
            });
            return this;
        }

        /// <summary>
        /// Adds an html control to grid panel.
        /// </summary>
        public DataGridControl<TModel, TElement> AddToPanel(IHtmlString control)
        {
            RenderInfo.PanelControls.Add(control);
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