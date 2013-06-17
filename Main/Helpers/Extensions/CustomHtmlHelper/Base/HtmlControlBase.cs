using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace SDM.Main.Helpers.Extensions.CustomHtmlHelper.Base
{
    /// <summary>
    /// Provides foundation for HTML control
    /// </summary>
    public abstract class HtmlControlBase<T> : IHtmlString
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
        protected HtmlHelper<T> Helper { get; private set; }

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
        protected HtmlControlBase(HtmlHelper<T> helper)
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
        public static IHtmlString operator +(HtmlControlBase<T> c1, IHtmlString c2)
        {
            return new HtmlString(c1.ToHtmlString().TrimEnd() + c2.ToHtmlString().TrimStart());
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
            // get absolute path
            // ReSharper disable PossibleNullReferenceException
            string path = string.Format("~/{0}/{1}.cshtml", this.GetType().Namespace.Replace("SDM.Main.", string.Empty).Replace('.', '/'), name);
            // ReSharper restore PossibleNullReferenceException

            // render
            return this.Helper.Partial(path, model);
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