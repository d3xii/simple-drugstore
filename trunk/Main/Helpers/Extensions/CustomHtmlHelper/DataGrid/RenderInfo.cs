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
        /// Gets or sets name of the grid control.
        /// </summary>
        public string Name = "slickGrid";

        /// <summary>
        /// Gets or sets width of the grid.
        /// </summary>
        public string Width;

        /// <summary>
        /// Gets or sets width of the grid.
        /// </summary>
        public string Height;

        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        public ICollection DataSource;

        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        public readonly List<ColumnInfo> Columns = new List<ColumnInfo>();

        /// <summary>
        /// Gets or sets the buttons.
        /// </summary>
        public readonly List<ButtonInfo> Buttons = new List<ButtonInfo>();

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
        }

        /// <summary>
        /// Contains button information.
        /// </summary>
        public class ButtonInfo
        {
            public string DisplayText;
            public string ActionName;
            public string ControllerName;
        }

        #endregion
    }
}