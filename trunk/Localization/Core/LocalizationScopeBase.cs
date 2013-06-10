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
        public SharedTexts.SharedTexts Shared { get; private set; }

        #region Implementation of ILocalizationScope

        /// <summary>
        /// Sets reference to the shared texts.
        /// </summary>
        void ILocalizationScope.SetSharedTextsReference(object obj)
        {
            Shared = (SharedTexts.SharedTexts) obj;
        }

        #endregion
    }
}