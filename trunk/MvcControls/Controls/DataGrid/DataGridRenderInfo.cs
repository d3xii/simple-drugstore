using System.Collections;
using System.Collections.Generic;
using System.Web;

namespace MvcControls.Controls.DataGrid
{
    /// <summary>
    /// Contains information necessary to render the data grid.
    /// </summary>
    public class DataGridRenderInfo
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
        public string Name = "grid";

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
        public readonly List<DataGridColumnInfo> Columns = new List<DataGridColumnInfo>();

        /// <summary>
        /// Gets or sets list of html controls on the grid panel.
        /// </summary>
        public readonly List<IHtmlString> PanelControls = new List<IHtmlString>();

        /// <summary>
        /// Gets or sets the edit mode of the grid.
        /// </summary>
        public IDataGridEditModule EditModule = new DataGridNoEditModule();

        #endregion
    }
}