using System.Web;
using System.Web.Mvc;

namespace MvcControls.Controls.DataGrid
{
    /// <summary>
    /// Defines fake Edit Module for data grid.
    /// This module practically does nothing.
    /// </summary>
    public class DataGridNoEditModule : IDataGridEditModule
    {
        #region Implementation of IDataGridEditModule
        /// <summary>
        /// Renders new line row.
        /// </summary>
        public IHtmlString RenderNewLineRow()
        {
            return null;
        }

        /// <summary>
        /// Renders initialize code.
        /// </summary>
        public IHtmlString RenderInitializeCode()
        {
            return null;
        }

        /// <summary>
        /// Initializes this module before it is ready to be used to render HTML.
        /// </summary>
        public void InitializeBeforeRender(HtmlHelper helper, DataGridRenderInfo renderInfo)
        {
        }

        #endregion
    }
}