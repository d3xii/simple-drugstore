using System;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using MvcControls.Controls.Base;
using MvcControls.Controls.Parameters;

namespace MvcControls.Controls.DataGrid
{
    /// <summary>
    /// Defines "Inline" form edit mode of the data grid.
    /// Visit http://mvc.devexpress.com/GridView/EditModes for illustration.
    /// This base class is used to support non-generic type view.
    /// </summary>
    public abstract class DataGridInlineFormEditModuleBase : IDataGridEditModule
    {
        //**************************************************
        //
        // Public variables
        //
        //**************************************************

        #region Public variables

        // ReSharper disable MemberCanBePrivate.Global

        /// <summary>
        /// Gets or sets selector to select the property represents the row ID.
        /// </summary>
        protected Func<object, string> RowIdPropertySelector { get; set; }

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

        /// <summary>
        /// Gets or sets a value indicates whether the Edit button in each line of the button is visible.
        /// </summary>
        public bool IsEditButtonVisible { get; set; }

        /// <summary>
        /// Gets or sets the content of the edit button.
        /// </summary>
        public IHtmlString EditButtonText { get; set; }

        /// <summary>
        /// Gets or sets the template of the command column header.
        /// </summary>
        public Func<DataGridInlineFormEditModuleBase, HelperResult> CommandColumnHeaderTemplate { get; set; }

        #endregion


        //**************************************************
        //
        // Private properties
        //
        //**************************************************

        #region Private properties

        /// <summary>
        /// Indicates whether the command cell is visible to render.
        /// </summary>
        private bool IsCommandCellVisible
        {
            get
            {
                // if not visible
                return this.IsEditButtonVisible;
            }
        }

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
        protected DataGridInlineFormEditModuleBase()
        {
            this.NewLineDisplayContent = new HtmlString("Click here to add new row");
            this.SaveButtonText = "Save";
            this.SaveNewButtonText = "Save & New";
            this.CancelButtonText = "Cancel";
            this.EditButtonText = new HtmlString("Edit");
            this.IsEditButtonVisible = true;
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
        IHtmlString IDataGridEditModule.RenderInitializeCode()
        {
            Debug.Assert(this.NewLineContentUrl != null);
            return RenderHelper.RenderTemplate(this.Helper, "DataGrid.InlineForm.Initialize", this);
        }

        /// <summary>
        /// Initializes this module before it is ready to be used to render HTML.
        /// </summary>
        void IDataGridEditModule.InitializeBeforeRender(HtmlHelper helper, DataGridRenderInfo renderInfo)
        {
            this.Helper = helper;
            this.RenderInfo = renderInfo;
        }

        /// <summary>
        /// Gets number of addtional cells.
        /// </summary>
        public int AddtionalCellCount { get { return 1; } }

        /// <summary>
        /// Renders new line row.
        /// </summary>
        IHtmlString IDataGridEditModule.RenderNewLineRow()
        {
            return RenderHelper.RenderTemplate(this.Helper, "DataGrid.InlineForm.NewLine", this);
        }

        /// <summary>
        /// Renders command column header.
        /// </summary>
        IHtmlString IDataGridEditModule.RenderCommandColumnHeader()
        {
            // if not visible
            if (!this.IsCommandCellVisible)
            {
                // do not show
                return null;
            }

            return this.CommandColumnHeaderTemplate != null ? (IHtmlString)this.CommandColumnHeaderTemplate(this) : new HtmlString("<th></th>");
        }

        /// <summary>
        /// Renders command cell.
        /// </summary>
        IHtmlString IDataGridEditModule.RenderCommandCell()
        {
            // if not visible
            if (!this.IsCommandCellVisible)
            {
                // do not show
                return null;
            }

            return RenderHelper.RenderTemplate(this.Helper, "DataGrid.InlineForm.CommandCell", this);
        }

        /// <summary>
        /// Renders row id.
        /// </summary>
        string IDataGridEditModule.RenderRowId(object rowObject)
        {
            // if not visible
            if (!this.IsCommandCellVisible)
            {
                // do not show
                return null;
            }

            return this.RowIdPropertySelector(rowObject);
        }

        #endregion     
    }
}