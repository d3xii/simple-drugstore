using System;
using System.Web;

namespace MvcControls.Controls.DataGrid
{
    /// <summary>
    /// Contains column information.
    /// </summary>
    public class DataGridColumnInfo
    {
        public string Name;
        public string DisplayText;
        public Func<object, object> PropertyValueSelector;
        public Func<object, IHtmlString> HtmlRenderer;
    }
}