namespace SDM.Localization.Core
{
    /// <summary>
    /// Provides foundation for all location scope.
    /// </summary>
    public abstract class LocalizationScopeBase : ILocalizationScope
    {
        /// <summary>
        /// Gets reference to shared texts.
        /// </summary>
        public SharedTexts.SharedTexts Shared { get; internal set; }      
    }
}