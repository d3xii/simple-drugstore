using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using MvcControls.Controls.Base;
using MvcControls.Controls.Parameters;

namespace MvcControls.Controls.DataGrid
{
    /// <summary>
    /// Defines "Inline" form edit mode of the data grid.
    /// Visit http://mvc.devexpress.com/GridView/EditModes for illustration.
    /// </summary>
    public class DataGridInlineFormEditModule : IDataGridEditModule
    {
        //**************************************************
        //
        // Public variables
        //
        //**************************************************

        #region Public variables

        // ReSharper disable MemberCanBePrivate.Global

        /// <summary>
        /// Gets or sets content displayed in the new line row.
        /// </summary>
        public IHtmlString NewLineDisplayContent { get; set; }        

        /// <summary>
        /// Gets or sets action which returns new line content.
        /// </summary>
        public ActionParameter NewLineContentUrl { get; set; }

        /// <summary>
        /// Gets or sets text of save button.
        /// </summary>
        public string SaveButtonText { get; set; }

        /// <summary>
        /// Gets or sets text of save button.
        /// </summary>
        public string SaveNewButtonText { get; set; }

        /// <summary>
        /// Gets or sets text of cancel button.
        /// </summary>
        public string CancelButtonText { get; set; }

        #endregion


        //**************************************************
        //
        // Constructors
        //
        //**************************************************

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public DataGridInlineFormEditModule()
        {
            this.NewLineDisplayContent = new HtmlString("Click here to add new row");
            this.SaveButtonText = "Save";
            this.SaveNewButtonText = "Save & New";
            this.CancelButtonText = "Cancel";
        }

        #endregion


        #region Implementation of IDataGridEditModule

        /// <summary>
        /// Gets the HTML helper associated with this instance.
        /// This property is used to support the underlying system and should not be used directly.
        /// </summary>
        public HtmlHelper Helper { get; private set; }

        /// <summary>
        /// Gets or sets the data grid render information.
        /// This property is used to support the underlying system and should not be used directly.
        /// </summary>
        public DataGridRenderInfo RenderInfo { get; private set; }

        /// <summary>
        /// Renders initialize code.
        /// </summary>
        public IHtmlString RenderInitializeCode()
        {
            Debug.Assert(this.NewLineContentUrl != null);
            return RenderHelper.RenderTemplate(this.Helper, "DataGrid.InlineForm.Initialize", this);
        }

        /// <summary>
        /// Initializes this module before it is ready to be used to render HTML.
        /// </summary>
        public void InitializeBeforeRender(HtmlHelper helper, DataGridRenderInfo renderInfo)
        {
            this.Helper = helper;
            this.RenderInfo = renderInfo;
        }

        /// <summary>
        /// Renders new line row.
        /// </summary>
        public IHtmlString RenderNewLineRow()
        {
            return RenderHelper.RenderTemplate(this.Helper, "DataGrid.InlineForm.NewLine", this);
        }

        #endregion
    }
}