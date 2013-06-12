using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SDM.Domain.Config
{
    /// <summary>
    /// Contains SQL-related configurations.
    /// </summary>
    public class SqlConfigModel
    {
        //**************************************************
        //
        // Public variables
        //
        //**************************************************

        #region Public variables

        /// <summary>
        /// Gets or sets server name.
        /// </summary>
        [DisplayName("Server Name"), DefaultValue("localhost"), Required]
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets SQL database name.
        /// </summary>
        [DisplayName("Database Name"), DefaultValue(""), Required]
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets SQL Server user name.
        /// </summary>
        [DisplayName("User Name"), DefaultValue("sa"), Required]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets SQL Server password.
        /// </summary>        
        [DefaultValue(""), PasswordPropertyText(true), Required(AllowEmptyStrings = true)]
        public string Password { get; set; }

        #endregion


        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods

        /// <summary>
        /// Validates this model against the business rules.
        /// </summary>
        public ModelValidationResult<SqlConfigModel> Validate()
        {
            return new ModelValidationResult<SqlConfigModel>().ValidateByUsingDataAnnotation(this);
        }

        /// <summary>
        /// Checks whether the password is specified in this instance.
        /// If the Password is NULL, it means the user didnt change it.
        /// </summary>
        public bool IsPasswordDefined()
        {
            return !string.IsNullOrEmpty(this.Password);
        }

        #endregion

    }
}