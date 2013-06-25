using System.Data.Common;
using SDM.Domain.Models;
using SDM.Infrastructure.Database;
using SDM.Services.Base;

namespace SDM.Services.Database
{
    /// <summary>
    /// Provides methods to setup the database for first-use.
    /// </summary>
    public class DatabaseFormatter : ServiceRunnerBase
    {
        //**************************************************
        //
        // Public variables
        //
        //**************************************************

        #region Public variables

        /// <summary>
        /// Gets or sets to of this instance.
        /// </summary>
        public object Tag { get; set; }

        #endregion


        //**************************************************
        //
        // Private variables
        //
        //**************************************************

        #region Private variables

        /// <summary>
        /// Gets database context used by this instance.
        /// </summary>
        private readonly DatabaseContext _context;

        #endregion


        //**************************************************
        //
        // Constructors
        //
        //**************************************************

        #region Constructors

        /// <summary>
        /// Initializes new instance of <see cref="DatabaseFormatter"/>.
        /// </summary>
        public DatabaseFormatter(DatabaseContext context)
        {
            _context = context;
        }

        #endregion



        #region Overrides of ServiceRunnerBase

        /// <summary>
        /// Fired when the thread is running.
        /// </summary>
        protected override void OnExecute()
        {
            this.Write("Formatting database...");
            using (DbCommand command = _context.Database.Connection.CreateCommand())
            {
                _context.Database.Connection.Open();
//                command.CommandText = @"
//begin transaction
//EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'
//EXEC sp_msforeachtable 'drop table ?'
//EXEC sp_msforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all'
//if object_id('__MigrationHistory', 'U') is not null drop table __MigrationHistory;
//commit transaction
//";
                command.CommandText = @"
if object_id('__MigrationHistory', 'U') is not null drop table __MigrationHistory;
EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL' 
EXEC sp_msforeachtable 'drop table ?'
EXEC sp_msforeachtable 'drop table ?'
EXEC sp_msforeachtable 'drop table ?'
EXEC sp_msforeachtable 'drop table ?'
";
                command.ExecuteNonQuery();
                _context.Database.Connection.Close();
            }

            this.Write("Creating default account (name: admin, password: admin)...");
            this.AddAccount(_context, "admin", "admin");
        }

        #endregion


        //**************************************************
        //
        // Private methods
        //
        //**************************************************

        #region Private methods

        /// <summary>
        /// Adds an account to repository.
        /// </summary>
        private void AddAccount(DatabaseContext context, string userName, string password)
        {
            // create new model
            AccountModel model = new AccountModel
                                     {
                                         UserName = userName,
                                         Password = password,
                                         IsAdmin = true,
                                         IsEnabled = true,
                                     };

            // add to repository
            context.Accounts.Add(model);
        }

        #endregion

    }
}
