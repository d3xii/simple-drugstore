using System;
using System.Collections;
using System.Collections.Generic;

namespace SDM.Main.Helpers.Extensions.CustomHtmlHelper.DataGrid
{
    /// <summary>
    /// Contains information necessary to render the data grid.
    /// </summary>
    public class RenderInfo
    {
        //**************************************************
        //
        // Public variables
        //
        //**************************************************

        #region Public variables

        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        public ICollection DataSource;

        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        public readonly List<ColumnInfo> Columns = new List<ColumnInfo>();

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
            public string DisplayName;
            public Func<object, object> ValueSelector;
            public bool IsVisible;
        }

        #endregion
    }
}