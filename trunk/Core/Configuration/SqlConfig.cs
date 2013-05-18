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
        [DefaultValue("localhost")]
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets SQL database name.
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets SQL Server user name.
        /// </summary>
        [DefaultValue("sa")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets SQL Server password.
        /// </summary>        
        public string Password { get; set; }
    }
}