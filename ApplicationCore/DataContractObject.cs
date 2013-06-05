using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace SDM.ApplicationCore
{
    /// <summary>
    /// Provides foundation for [DataContract] classes.
    /// </summary>
    [DataContract]
    public abstract class DataContractObject<T>
        where T : class
    {
        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods

        /// <summary>
        /// Saves object content to given file.
        /// </summary>
        public void SaveToFile(string filePath)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                this.SaveToStream(stream);
                stream.Flush();
            }
        }

        /// <summary>
        /// Saves object content to given stream.
        /// </summary>
        public void SaveToStream(Stream stream)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            serializer.WriteObject(stream, this);            
        }

        /// <summary>
        /// Loads object from given file.
        /// </summary>
        public static T LoadFromFile(string filePath)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return LoadFromStream(stream);                
            }
        }

        /// <summary>
        /// Loads object from given stream.
        /// </summary>
        public static T LoadFromStream(Stream stream)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            T result = (T) serializer.ReadObject(stream);
            return result;
        }

        /// <summary>
        /// Copies plain values from source to target.
        /// Processes deep copy.
        /// Ignores if [PasswordPropertyText(true)] properties with NULL value in the source object.
        /// </summary>
        public void CopyValues(T target)
        {
            this.CopyValues(this, target);
        }

        /// <summary>
        /// Sets default values for this object and its chidlren.
        /// This will Stackoverflow exception if there is circular dependency in the object.
        /// </summary>
        public T ResetValues()
        {
            this.ResetValues(this);
            return this as T;
        }

        #endregion

        
        //**************************************************
        //
        // Private methods
        //
        //**************************************************

        #region Private methods

        /// <summary>
        /// Fired when the object is deserialized.
        /// </summary>
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {            
            //this.ResetValues();
        }

        /// <summary>
        /// Copies plain values from source to target.
        /// Processes deep copy.
        /// Ignores if [PasswordPropertyText(true)] properties with NULL value in the source object.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        private void CopyValues(object source, object target)
        {
            // for each property
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
            {
                // get its value
                object value = property.GetValue(source);

                // if it is password and NULL
                if (property.Attributes.OfType<PasswordPropertyTextAttribute>().Any())
                {
                    // get attribute
                    PasswordPropertyTextAttribute attribute = property.Attributes.OfType<PasswordPropertyTextAttribute>().First();

                    // if it is true and value is null
                    if (attribute.Password && value == null)
                    {
                        // do not copy this property
                        continue;
                    }
                }

                // if value is system type
                if (property.PropertyType.Module.ScopeName == "CommonLanguageRuntimeLibrary")
                {
                    // copy it
                    // ReSharper disable AssignNullToNotNullAttribute
                    property.SetValue(target, value);
                    // ReSharper restore AssignNullToNotNullAttribute
                }
                else
                {
                    // application custom types
                    // move deeper
                    this.CopyValues(value, property.GetValue(target));
                }
            }
        }

        /// <summary>
        /// Sets default values for given objects and its chidlren.
        /// This will Stackoverflow exception if there is circular dependency in the object.
        /// </summary>
        private void ResetValues(object obj)
        {
            // for each property
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(obj))
            {
                // reset its value
                property.ResetValue(obj);

                // get its value
                object value = property.GetValue(obj);

                // if value is complex type
                if (!(value is ValueType) && property.PropertyType != typeof(string))
                {
                    // if it is null
                    if (value == null)
                    {
                        // try to initialize it
                        value = Activator.CreateInstance(property.PropertyType);
                        property.SetValue(obj, value);
                    }

                    // move deeper
                    ResetValues(value);
                }
            }
        }

        #endregion

    }
}