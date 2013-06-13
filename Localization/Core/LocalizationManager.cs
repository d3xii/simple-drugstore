using System;
using System.Collections.Generic;
using System.Linq;
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
        // ReSharper disable NotAccessedField.Local
        private static ILocalizationRoot _textsRoot;
        // ReSharper restore NotAccessedField.Local

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
        public static void Initialize(Assembly assembly, ILocalizationRoot root, Func<AssemblyName, bool> assemblyFilter = null)
        {
            // load texts from compiled source code
            _textsRoot = root;

            // initialize shortcut
            _shortcuts = new Dictionary<Type, ILocalizationScope>();

            // create default filter if not defined
            if (assemblyFilter == null)
            {
                assemblyFilter = t => true;
            }

            // scan all assemblies and get scopes
            HashSet<Assembly> scannedAssemblies = new HashSet<Assembly>();
            AddTranslationScopes(assembly, assemblyFilter, scannedAssemblies, root, _shortcuts);
        }

        /// <summary>
        /// Gets localized text from given key in limited scope.
        /// </summary>
        public static string GetTextFromScope<TLocalizationScope>(Func<TLocalizationScope, string> key)
            where TLocalizationScope : ILocalizationScope
        {
            // get type of the scope
            var type = typeof(TLocalizationScope);

            // check if the scope is register
            if (!_shortcuts.ContainsKey(type))
            {
                throw new InvalidOperationException(string.Format("Unable to get localizable text: Localizable scope not found: {0}.", type));
            }

            string result = key((TLocalizationScope)_shortcuts[type]);
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
                    AddShortcuts((ILocalizationScope)value, shortcuts);
                }
            }
        }

        /// <summary>
        /// Adds transaction scopes in given assembly and marks the assembly as scanned, then scans references of that assembly
        /// for additional scopes.
        /// </summary>
        private static void AddTranslationScopes(
            Assembly assembly,
            Func<AssemblyName, bool> assemblyFilter,
            HashSet<Assembly> scannedAssemblies,
            ILocalizationRoot root,
            Dictionary<Type, ILocalizationScope> shortcuts)
        {
            // skip if scanned
            if (scannedAssemblies.Contains(assembly))
            {
                // skip this
                return;
            }

            // getl all scopes
            Type[] scopeContainers = assembly
                .GetTypes()
                .Where(t => typeof (ILocalizationScope).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
                .ToArray();

            // for each scope container
            foreach (Type container in scopeContainers)
            {
                // create new instance
                ILocalizationScope createdContainer = (ILocalizationScope)Activator.CreateInstance(container);                
                createdContainer.SetSharedTextsReference(root.SharedText);

                // add to scope
                root.Scopes.Add(createdContainer);

                // add to shortcut
                AddShortcuts(createdContainer, shortcuts);
            }

            // mark this assembly as scanned
            scannedAssemblies.Add(assembly);

            // get all references assemblies
            AssemblyName[] subAssemblies = assembly.GetReferencedAssemblies().Where(assemblyFilter).ToArray();
            foreach (AssemblyName subAssemblyName in subAssemblies)
            {
                // scan inside
                AddTranslationScopes(Assembly.Load(subAssemblyName), assemblyFilter, scannedAssemblies, root, shortcuts);
            }
        }

        #endregion       
    }
}
