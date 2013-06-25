using MvcControls.Controls.Parameters;

namespace MvcControls.Controls.Button
{
    /// <summary>
    /// Contains information necessary to render the button.
    /// </summary>
    public class ButtonControlRenderInfo
    {
        /// <summary>
        /// Gets or sets button type. Default value is <see cref="ButtonType.Default"/>.
        /// </summary>
        public ButtonType Type;

        /// <summary>
        /// Gets or sets text of the button.
        /// </summary>
        public string Text;
        
        /// <summary>
        /// Gets or sets action of the button.
        /// </summary>
        public ActionParameter Action;

        /// <summary>
        /// Gets or sets the confirmation text will be shown when the user clicks the button.
        /// If NULL, no confirmation is shown.
        /// </summary>
        public string ConfirmationText;


        //**************************************************
        //
        // Nested classes
        //
        //**************************************************

        #region Nested classes

        /// <summary>
        /// Defines type of a button.
        /// </summary>
        public enum ButtonType
        {
            /// <summary>
            /// Represents a default button.
            /// </summary>
            Default,

            /// <summary>
            /// Represents a submit button without any details.
            /// </summary>
            Submit,

            /// <summary>
            /// Represents a normal button but will create a form and post it using JavaScript.
            /// </summary>
            Post
        }

        #endregion

    }
}