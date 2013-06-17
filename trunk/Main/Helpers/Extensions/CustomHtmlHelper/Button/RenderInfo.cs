namespace SDM.Main.Helpers.Extensions.CustomHtmlHelper.Button
{
    /// <summary>
    /// Contains information necessary to render the button.
    /// </summary>
    public class RenderInfo
    {
        public bool IsSubmitButton;
        public string DisplayText;
        public string ActionName;
        public string ControllerName;
        public object RouteValues;
    }
}