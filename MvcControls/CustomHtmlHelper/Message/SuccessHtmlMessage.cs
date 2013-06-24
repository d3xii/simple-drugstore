namespace MvcControls.CustomHtmlHelper.Message
{
    /// <summary>
    /// Renders an success html message.
    /// </summary>
    internal class SuccessHtmlMessage : HtmlMessageBase
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
        public SuccessHtmlMessage(string message)
            : base(message)
        {
        }

        #endregion


        #region Implementation of IHtmlMessage

        /// <summary>
        /// Renders this html message to MVC stream.
        /// </summary>
        public override HtmlString Render(HtmlHelper helper)
        {
            return new HtmlString(string.Format("<span class='message-success'>{0}</span>", helper.Encode(this.Message)));
        }

        #endregion
    }
}