using System;
using System.Web;
using System.Web.Mvc;

namespace MvcControls.Controls.Base
{
    /// <summary>
    /// Provides foundation for HTML control
    /// </summary>
    public abstract class HtmlControlBase<TModel, TRenderInfo> : IHtmlString
        where TRenderInfo : new()
    {
        //**************************************************
        //
        // Protected variables
        //
        //**************************************************

        #region Protected variables

        /// <summary>
        /// Gets the HTML helper associated with this instance.
        /// </summary>
        protected HtmlHelper<TModel> Helper { get; private set; }

        /// <summary>
        /// Gets model instance associated with this instance.
        /// </summary>
        protected TModel Model
        {
            get { return this.Helper.ViewData.Model; }
        }

        /// <summary>
        /// Gets or sets the internal render information that will be built up by using fluent interface.
        /// </summary>
        protected readonly TRenderInfo RenderInfo = new TRenderInfo();

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
        protected HtmlControlBase(HtmlHelper<TModel> helper)
        {
            Helper = helper;
        }

        #endregion


        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods

        /// <summary>
        /// Joins the HTML string of 2 controls.
        /// </summary>
        public static IHtmlString operator +(HtmlControlBase<TModel, TRenderInfo> c1, IHtmlString c2)
        {
            return new HtmlString(c1.ToHtmlString().TrimEnd() + c2.ToHtmlString().TrimStart());
        }        

        /// <summary>
        /// Sets render information.
        /// </summary>
        public HtmlControlBase<TModel, TRenderInfo> Settings(Action<TRenderInfo> renderInfoSetter)
        {
            renderInfoSetter(this.RenderInfo);
            return this;
        }

        #endregion



        //**************************************************
        //
        // Protected methods
        //
        //**************************************************

        #region Protected methods

        /// <summary>
        /// Renders given partial view within the same folder of the control.
        /// </summary>
        protected IHtmlString RenderPartial(string name, object model)
        {
            return RenderHelper.RenderTemplate(this.Helper, name, model);
        }

        #endregion


        #region Implementation of IHtmlString

        /// <summary>
        /// Returns an HTML-encoded string.
        /// </summary>
        /// <returns>
        /// An HTML-encoded string.
        /// </returns>
        public abstract string ToHtmlString();

        #endregion
    }
}