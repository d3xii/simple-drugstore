using System;

namespace MvcControls.Controls.DataGrid
{
    /// <summary>
    /// Defines "Inline" form edit mode of the data grid.
    /// Visit http://mvc.devexpress.com/GridView/EditModes for illustration.
    /// This generic-type class is used to pass to user customization.
    /// </summary>
    public class DataGridInlineFormEditModule<TElement> : DataGridInlineFormEditModuleBase
    {
        /// <summary>
        /// Gets or sets selector to select the property represents the row ID.
        /// </summary>
        public new Func<TElement, object> RowIdPropertySelector
        {
            set { base.RowIdPropertySelector = t => value((TElement) t).ToString(); }
        }
    }
}