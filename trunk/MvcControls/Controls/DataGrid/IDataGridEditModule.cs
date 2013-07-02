using System.Web;
using System.Web.Mvc;

namespace MvcControls.Controls.DataGrid
{
    /// <summary>
    /// Defines the support methods to enable edit mode in the data grid.
    /// </summary>
    public interface IDataGridEditModule
    {
        /// <summary>
        /// Renders new line row.
        /// </summary>
        IHtmlString RenderNewLineRow();

        /// <summary>
        /// Renders initialize code.
        /// </summary>
        IHtmlString RenderInitializeCode();

        /// <summary>
        /// Initializes this module before it is ready to be used to render HTML.
        /// </summary>
        void InitializeBeforeRender(HtmlHelper helper, DataGridRenderInfo renderInfo);

        /// <summary>
        /// Renders command column header.
        /// </summary>
        IHtmlString RenderCommandColumnHeader();

        /// <summary>
        /// Renders command cell.
        /// </summary>
        IHtmlString RenderCommandCell();

        /// <summary>
        /// Renders row id.
        /// </summary>
        string RenderRowId(object rowObject);
    }
}