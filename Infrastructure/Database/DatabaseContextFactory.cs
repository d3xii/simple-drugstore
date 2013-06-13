using System;
using System.Data.SqlClient;
using SDM.Core.Localization;
using SDM.Domain.Config;
using SDM.Localization.Core;

namespace SDM.Infrastructure.Database
{
    /// <summary>
    /// Provides methods to generate <see cref="DatabaseContext"/>
    /// </summary>
    public class DatabaseContextFactory : ILocalizable<DatabaseContextFactory.Texts>
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
            // create context
            DatabaseContext context = new DatabaseContext(this.GetSqlConnection(sqlConfig));
            return context;
        }

        /// <summary>
        /// Gets SQL Connection from given config.
        /// </summary>
        public SqlConnection GetSqlConnection(SqlConfigModel sqlConfig)
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

            // return result
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// Tests SQL Connection from given config and results the error message.
        /// Returns NULL if success.
        /// </summary>
        public string TestConnectionString(SqlConfigModel sqlConfig)
        {
            // get sql connection
            try
            {
                object id;
                using (SqlConnection sqlConnection = this.GetSqlConnection(sqlConfig))
                {
                    sqlConnection.Open();
                    using (SqlCommand command = sqlConnection.CreateCommand())
                    {
                        command.CommandText = string.Format("select db_id('{0}')", sqlConfig.DatabaseName);
                        id = command.ExecuteScalar();
                    }
                }

                // if not null => database exists
                if (id != null && id != DBNull.Value)
                {
                    // exists
                    return null;
                }

                // not exists
                return string.Format(this.Localize(t => t.DatabaseNotFound), sqlConfig.DatabaseName);
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

        #endregion


        //**************************************************
        //
        // Nested classes
        //
        //**************************************************

        #region Nested classes

        public class Texts : CustomLocalizationScopeBase
        {
            public string DatabaseNotFound = "Database not found: {0}.";
        }

        #endregion

    }
}
