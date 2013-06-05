using System.Data.SqlClient;
using SDM.ApplicationServices.Configuration;

namespace SDM.Infrastructure.Database
{
    /// <summary>
    /// Provides methods to generate <see cref="DatabaseContext"/>
    /// </summary>
    public class DatabaseContextFactory
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
        public DatabaseContext CreateContext(SqlConfigModel sqlConfig)
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
