using System;
using SDM.ApplicationCore.Console;
using SDM.ApplicationCore.ModelRepositories;
using SDM.Domain.Factories.Common;

namespace SDM.ApplicationServices.Database
{
    /// <summary>
    /// Provides methods to setup the database for first-use.
    /// </summary>
    public class DatabaseSetupService : ConsoleRunnerBase
    {
        //**************************************************
        //
        // Inputs
        //
        //**************************************************

        #region Inputs

        /// <summary>
        /// Gets or sets input of this service.
        /// </summary>
        public IAccountRepository InputAccountRepository;

        /// <summary>
        /// Gets or sets to of this instance.
        /// </summary>
        public object Tag { get; set; }

        #endregion


        #region Overrides of ConsoleRunnerBase

        /// <summary>
        /// Fired when the thread is running.
        /// </summary>
        protected override void OnExecute()
        {
            this.Write("Creating accounts...");
            this.AddAccount(this.InputAccountRepository, "admin", "admin");
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
