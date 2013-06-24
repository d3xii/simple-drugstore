using System.Web;
using System.Web.Mvc;

namespace MvcControls.Controls.Message
{
    /// <summary>
    /// Provides foundation for all HTML messages.
    /// </summary>
    public abstract class HtmlMessageBase : IHtmlMessage
    {
        #region Implementation of IHtmlMessage

        /// <summary>
        /// Gets raw message.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Renders this html message to MVC stream.
        /// </summary>
        public abstract IHtmlString Render(HtmlHelper helper);

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
        protected HtmlMessageBase(string message)
        {
            Message = message;
        }

        #endregion

    }
}