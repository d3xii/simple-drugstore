using System.ComponentModel;
using SDM.Core;

namespace SDM.Domain.Config
{
    /// <summary>
    /// Contains application config.
    /// </summary>
    public class ConfigModel : DataContractObject<ConfigModel>
    {
        //**************************************************
        //
        // Public variables
        //
        //**************************************************

        #region Public variables

        /// <summary>
        /// Gets or sets SQL config.
        /// </summary>
        public SqlConfigModel Sql { get; set; }

        /// <summary>
        /// Gets or sets admin password.
        /// </summary>        
        [DisplayName("Admin Password"), DefaultValue("admin"), PasswordPropertyText(true)]
        public string AdminPassword { get; set; }

        #endregion


        //**************************************************
        //
        // Constructors
        //
        //**************************************************

        #region Constructors

        /// <summary>
        /// Initializes new instance of <see cref="ConfigModel"/>.
        /// </summary>
        public ConfigModel()
        {
            this.ResetValues();
        }

        #endregion


        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods

        /// <summary>
        /// Checks whether the admin password was changed.
        /// </summary>
        public bool IsAdminPasswordChanged(ConfigModel target)
        {
            return target.AdminPassword != null && this.AdminPassword != target.AdminPassword;
        }

        #endregion

    }
}
