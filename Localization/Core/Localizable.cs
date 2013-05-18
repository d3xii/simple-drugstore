using System;
using System.Linq.Expressions;

// ReSharper disable CheckNamespace
namespace SDM.Localization.Core
{
    /// <summary>
    /// Contains extension methods for Localization.
    /// </summary>
    public static class Localizable
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
                                                          Expression<Func<TLocalizationScope, string>> key) where TLocalizationScope : ILocalizationScope
        {
            return LocalizationManager.GetTextFromScope(localizableClass, key);
        }

        #endregion
    }
}
// ReSharper restore CheckNamespace