using System.Data.Common;
using SDM.ApplicationCore.Console;
using SDM.ApplicationCore.ModelRepositories;
using SDM.Domain.Factories.Common;
using SDM.Infrastructure.Database.Repositories;

namespace SDM.Infrastructure.Database
{
    /// <summary>
    /// Provides methods to setup the database for first-use.
    /// </summary>
    public class DatabaseFormatter : ConsoleRunnerBase
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



        #region Overrides of ConsoleRunnerBase

        /// <summary>
        /// Fired when the thread is running.
        /// </summary>
        protected override void OnExecute()
        {
            this.Write("Formatting database...");
            using (DbCommand command = _context.Database.Connection.CreateCommand())
            {
                _context.Database.Connection.Open();
                command.CommandText = @"
begin transaction
EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'
EXEC sp_msforeachtable 'drop table ?'
EXEC sp_msforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all'
drop table __MigrationHistory
commit transaction
";
                command.ExecuteNonQuery();
                _context.Database.Connection.Close();
            }

            this.Write("Creating accounts...");
            this.AddAccount(new AccountRepository(_context), "admin", "admin");
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
        private void AddAccount(IAccountRepository accountRepository, string userName, string password)
        {
            accountRepository.Add(new AccountFactory().Create(userName, password));
        }

        #endregion

    }
}
