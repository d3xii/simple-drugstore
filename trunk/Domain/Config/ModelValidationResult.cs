using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SDM.Domain.Config
{
    /// <summary>
    /// Contains result of model validation.
    /// </summary>
    public class ModelValidationResult<T>
    {
        //**************************************************
        //
        // Public variables
        //
        //**************************************************

        #region Public variables

        /// <summary>
        /// Gets list of invalid properties and their error message based on the Data Annotation.
        /// </summary>
        public readonly Dictionary<PropertyDescriptor, string> InvaildProperties = new Dictionary<PropertyDescriptor, string>();

        #endregion


        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods

        ///// <summary>
        ///// Adds invalid property and associated message.
        ///// </summary>
        //public void AddInvalidProperty(Expression<Func<T, object>> property, string message)
        //{
        //    this.InvaildProperties.Add(property, message);
        //}

        /// <summary>
        /// Adds invalid property and associated message.
        /// </summary>
        public void AddInvalidProperty(PropertyDescriptor property, string message)
        {
            this.InvaildProperties.Add(property, message);
        }

        /// <summary>
        /// Validates given model by using .NET standard data annotations.
        /// </summary>
        public ModelValidationResult<T> ValidateByUsingDataAnnotation(T model)
        {
            // get all properties of this model
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(model);

            // for each property
            foreach (PropertyDescriptor property in properties)
            {
                // get property value
                object value = property.GetValue(model);

                // if [Required] is defined
                RequiredAttribute requiredAttribute = (RequiredAttribute)property.Attributes[typeof(RequiredAttribute)];
                if (requiredAttribute != null)
                {
                    if (!requiredAttribute.IsValid(value))
                    {
                        // add to error   
                        this.AddInvalidProperty(property, requiredAttribute.ErrorMessage);
                    }
                }
            }

            // fluent interface
            return this;
        }

        #endregion
    }
}
