using System;
using System.Linq.Expressions;

namespace SDM.Core.Localization
{
    /// <summary>
    /// Provides methods to localize texts.
    /// </summary>
    internal class LocalizationManager
    {
        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods

        /// <summary>
        /// Gets text from given key.
        /// </summary>
        public string GetText(Expression<Func<LocalizationKeysRoot, string>> key)
        {
            // TODO: implement localization file reader
            string result = key.Compile()(new LocalizationKeysRoot());
            return result;
        }

        #endregion
    }
}