using System.ComponentModel;

namespace SDM.Core.Configuration
{
    /// <summary>
    /// Contains SQL-related configurations.
    /// </summary>
    public class SqlConfig
    {
        /// <summary>
        /// Gets or sets server name.
        /// </summary>
        [DisplayName("Server Name"), DefaultValue("localhost")]
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets SQL database name.
        /// </summary>
        [DisplayName("Database Name"), DefaultValue("")]
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets SQL Server user name.
        /// </summary>
        [DisplayName("User Name"), DefaultValue("sa")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets SQL Server password.
        /// </summary>        
        [DefaultValue(""), PasswordPropertyText(true)]
        public string Password { get; set; }
    }
}