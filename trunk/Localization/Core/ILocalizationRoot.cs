using System.Collections.Generic;

namespace SDM.Localization.Core
{
    /// <summary>
    /// Defines starting point for localization texts.
    /// </summary>
    public interface ILocalizationRoot
    {
        /// <summary>
        /// Gets reference to the shared text.
        /// </summary>
        ISharedTexts SharedText { get; }

        /// <summary>
        /// Gets available translatation scopes in all assemblies.
        /// </summary>
        List<ILocalizationScope> Scopes { get; }
    }
}