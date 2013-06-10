namespace SDM.Localization.Core
{
    /// <summary>
    /// Limites the localization texts in the the scope.
    /// </summary>
    public interface ILocalizationScope
    {
        /// <summary>
        /// Sets reference to the shared texts.
        /// </summary>
        void SetSharedTextsReference(object obj);
    }
}