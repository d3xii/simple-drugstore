using System.Data.SqlClient;
using SDM.Core.Configuration;

namespace SDM.Core.Database
{
    /// <summary>
    /// Provides methods to manage database connections.
    /// </summary>
    public class DatabaseManager
    {
        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods

        /// <summary>
        /// Gets database context.
        /// </summary>
        public DatabaseContext CreateContext(SqlConfig sqlConfig)
        {
            // create connection string
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
                {
                    DataSource = sqlConfig.ServerName,
                    InitialCatalog = sqlConfig.DatabaseName,
                    UserID = sqlConfig.UserName,
                    Password = sqlConfig.Password,
                    IntegratedSecurity = false
                };
            string connectionString = builder.ToString();

            // create context
            DatabaseContext context = new DatabaseContext(connectionString);
            return context;
        }

        #endregion

    }
}
