namespace SDM.Main.Helpers.Extensions.CustomHtmlHelper
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