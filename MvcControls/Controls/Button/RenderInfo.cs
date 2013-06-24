using MvcControls.Controls.Parameters;

namespace MvcControls.Controls.Button
{
    /// <summary>
    /// Contains information necessary to render the button.
    /// </summary>
    public class RenderInfo
    {
        public ButtonType Type;
        public string DisplayText;
        public ActionParameter Action;
        public string ConfirmationText;

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
    }
}