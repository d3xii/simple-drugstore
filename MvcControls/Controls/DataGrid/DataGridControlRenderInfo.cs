using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;

namespace MvcControls.Controls.DataGrid
{
    /// <summary>
    /// Contains information necessary to render the data grid.
    /// </summary>
    public class DataGridControlRenderInfo
    {
        //**************************************************
        //
        // Public variables
        //
        //**************************************************

        #region Public variables

        /// <summary>
        /// Gets or sets name of the grid control.
        /// </summary>
        public string Name = "slickGrid";

        /// <summary>
        /// Gets or sets width of the grid.
        /// </summary>
        public string Width;

        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        public ICollection DataSource;

        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        public readonly List<ColumnInfo> Columns = new List<ColumnInfo>();

        /// <summary>
        /// Gets or sets list of html controls on the grid panel.
        /// </summary>
        public readonly List<IHtmlString> PanelControls = new List<IHtmlString>();

        #endregion


        //**************************************************
        //
        // Nested classes
        //
        //**************************************************

        #region Nested classes

        /// <summary>
        /// Contains column information.
        /// </summary>
        public class ColumnInfo
        {
            public string Name;
            public string DisplayText;
            public Func<object, object> PropertyValueSelector;
            public Func<object, IHtmlString> HtmlRenderer;
        }

        #endregion
    }
}