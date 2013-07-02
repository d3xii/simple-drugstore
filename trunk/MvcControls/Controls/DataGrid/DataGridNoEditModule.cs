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
        IHtmlString IDataGridEditModule.RenderNewLineRow()
        {
            return null;
        }

        /// <summary>
        /// Renders initialize code.
        /// </summary>
        IHtmlString IDataGridEditModule.RenderInitializeCode()
        {
            return null;
        }

        /// <summary>
        /// Initializes this module before it is ready to be used to render HTML.
        /// </summary>
        void IDataGridEditModule.InitializeBeforeRender(HtmlHelper helper, DataGridRenderInfo renderInfo)
        {
        }

        /// <summary>
        /// Renders command column header.
        /// </summary>
        IHtmlString IDataGridEditModule.RenderCommandColumnHeader()
        {
            return null;
        }

        /// <summary>
        /// Renders command cell.
        /// </summary>
        public IHtmlString RenderCommandCell()
        {
            return null;
        }

        /// <summary>
        /// Renders row id.
        /// </summary>
        public string RenderRowId(object rowObject)
        {
            return null;
        }

        #endregion
    }
}