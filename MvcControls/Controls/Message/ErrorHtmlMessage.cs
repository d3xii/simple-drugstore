using System.Web;
using System.Web.Mvc;

namespace MvcControls.Controls.Message
{
    /// <summary>
    /// Renders an error html message.
    /// </summary>
    public class ErrorHtmlMessage : HtmlMessageBase
    {
        //**************************************************
        //
        // Constructors
        //
        //**************************************************

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ErrorHtmlMessage(string message) : base(message)
        {
        }

        #endregion


        #region Implementation of IHtmlMessage

        /// <summary>
        /// Renders this html message to MVC stream.
        /// </summary>
        public override IHtmlString Render(HtmlHelper helper)
        {
            return new HtmlString(string.Format("<span class='message-error'>{0}</span>", helper.Encode(this.Message)));
        }

        #endregion
    }
}