namespace SDM.Localization.Core
{
    /// <summary>
    /// Provides foundation for all location scope.
    /// </summary>
    public abstract class LocalizationScopeBase<TSharedTexts> : ILocalizationScope
        where TSharedTexts : ISharedTexts
    {
        /// <summary>
        /// Gets reference to shared texts.
        /// </summary>
        public TSharedTexts Shared { get; private set; }

        #region Implementation of ILocalizationScope

        /// <summary>
        /// Sets reference to the shared texts.
        /// </summary>
        void ILocalizationScope.SetSharedTextsReference(ISharedTexts obj)
        {
            Shared = (TSharedTexts) obj;
        }

        #endregion
    }
}