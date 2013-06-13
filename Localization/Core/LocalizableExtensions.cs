using System;

// ReSharper disable CheckNamespace
namespace SDM.Localization.Core
{
    /// <summary>
    /// Contains extension methods for Localization.
    /// </summary>
    public static class LocalizableExtensions
    {
        //**************************************************
        //
        // Extension methods
        //
        //**************************************************

        #region Extension methods

        /// <summary>
        /// Shorthand to <see cref="LocalizationManager.GetTextFromScope{TLocalizationScope}"/>.
        /// </summary>
        public static string Localize<TLocalizationScope>(this ILocalizable<TLocalizationScope> localizableClass,
                                                          Func<TLocalizationScope, string> key) where TLocalizationScope : ILocalizationScope
        {
            return LocalizationManager.GetTextFromScope(key);
        }

        #endregion
    }
}
// ReSharper restore CheckNamespace