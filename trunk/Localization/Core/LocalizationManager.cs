﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace SDM.Localization.Core
{
    /// <summary>
    /// Provides methods to manage localization of a project.
    /// </summary>
    public static class LocalizationManager
    {
        //**************************************************
        //
        // Private variables
        //
        //**************************************************

        #region Private variables

        /// <summary>
        /// Gets or sets shortcuts to the texts root object.
        /// </summary>
        private static Dictionary<Type, ILocalizationScope> _shortcuts;

        #endregion


        //**************************************************
        //
        // Public variables
        //
        //**************************************************

        #region Public variables

        /// <summary>
        /// Gets the root to all localization data.
        /// </summary>
        private static ILocalizationScope TextsRoot { get; set; }

        #endregion


        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods

        /// <summary>
        /// Initializes the localization engine and loads default language from given texts.
        /// This method does not access the HDD.
        /// </summary>
        public static void Initialize(ILocalizationScope root)
        {
            // load texts from compiled source code
            TextsRoot = root;

            // popuplate all shortcuts
            _shortcuts = new Dictionary<Type, ILocalizationScope>();
            AddShortcuts(TextsRoot, _shortcuts);
        }

        /// <summary>
        /// Gets localized text from given key in limited scope.
        /// </summary>
        public static string GetTextFromScope<TLocalizationScope>(Expression<Func<TLocalizationScope, string>> key)
            where TLocalizationScope : ILocalizationScope
        {
            
            // get type of the scope
            var type = typeof (TLocalizationScope);

            // check if the scope is register
            if (!_shortcuts.ContainsKey(type))
            {
                throw new InvalidOperationException(string.Format("Unable to get localizable text: {0}. Localizable scope not found: {1}.", key, type));
            }

            string result = key.Compile()((TLocalizationScope) _shortcuts[type]);
            return result;
        }

        #endregion


        //**************************************************
        //
        // Private methods
        //
        //**************************************************

        #region Private methods

        /// <summary>
        /// Adds this object and its children into shortcuts dictionary.
        /// </summary>
        private static void AddShortcuts(ILocalizationScope obj, Dictionary<Type, ILocalizationScope> shortcuts)
        {
            // add object first
            shortcuts.Add(obj.GetType(), obj);

            // get all public fields
            FieldInfo[] fieldInfos = obj.GetType().GetFields();

            // for each fields
            foreach (var fieldInfo in fieldInfos)
            {
                // get its value        
                object value = fieldInfo.GetValue(obj);

                // if it is a localizable scope field
                if (value is ILocalizationScope)
                {
                    // move deeper
                    AddShortcuts((ILocalizationScope) value, shortcuts);
                }
            }
        }

        #endregion

    }
}