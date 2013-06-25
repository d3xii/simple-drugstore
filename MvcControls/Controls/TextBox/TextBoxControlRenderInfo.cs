using MvcControls.Controls.Parameters;

namespace MvcControls.Controls.TextBox
{
    /// <summary>
    /// Contains information necessary to render the button.
    /// </summary>
    public class TextBoxControlRenderInfo
    {
        /// <summary>
        /// Gets or sets text of the label at the left side of the text box.
        /// If NULL, no label is shown.
        /// </summary>
        public string LabelText;

        /// <summary>
        /// Gets or sets name of the property.
        /// </summary>
        public string PropertyName;

        /// <summary>
        /// Gets or sets value of the property.
        /// </summary>
        public object PropertyValue;

        /// <summary>
        /// Gets or sets JSON source of the data source.
        /// If NULL, autocomplete feature will be disabled.
        /// </summary>
        public ActionParameter AutoCompleteDataSource;
    }
}