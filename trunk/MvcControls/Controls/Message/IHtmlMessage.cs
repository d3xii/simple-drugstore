using MvcControls.Controls.Base;

namespace MvcControls.Controls.Message
{
    /// <summary>
    /// Defines a flexible HTML in-line message.
    /// </summary>
    public interface IHtmlMessage : IHtmlControl
    {
        /// <summary>
        /// Gets raw message.
        /// </summary>
        string Message { get; }
    }
}