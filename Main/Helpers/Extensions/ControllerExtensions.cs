using System.Web.Mvc;
using SDM.ApplicationServices.Configuration;
using SDM.Infrastructure.Database;
using SDM.Infrastructure.Database.Repositories;
using SDM.Infrastructure.Hdd;

namespace SDM.Main.Helpers.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="Controller"/> class.
    /// </summary>
    internal static class ControllerExtensions
    {
        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods

        /// <summary>
        /// Gets context can be used in common situation.
        /// </summary>
        public static ControllerContext GetContext(this Controller controller)
        {
            // get database context
            DatabaseContext databaseContext = GetDatabaseContext(controller);

            // return result
            return new ControllerContext
                       {
                           HttpContext = controller.HttpContext,
                           Database = databaseContext,
                           AccountRepository = new AccountRepository(databaseContext)
                       };
        }

        #endregion


        //**************************************************
        //
        // Private methods
        //
        //**************************************************

        #region Private methods

        /// <summary>
        /// Gets database context from saved config.
        /// </summary>
        private static DatabaseContext GetDatabaseContext(Controller controller)
        {
            // read from config
            ConfigRepository repository = new ConfigRepository(new FileAccessProvider(controller.Server));
            SqlConfigModel sqlConfigModel = repository.Load().Sql;

            // create context
            return new DatabaseContextFactory().CreateContext(sqlConfigModel);
        }

        #endregion        
    }
}