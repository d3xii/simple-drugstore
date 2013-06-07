using SDM.ApplicationCore.ModelRepositories;
using SDM.Domain.Factories.Common;

namespace SDM.ApplicationServices.Database
{
    /// <summary>
    /// Provides methods to setup the database for first-use.
    /// </summary>
    public class DatabaseSetupService
    {
        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods

        /// <summary>
        /// Adds default data to given database.
        /// </summary>
        public void Setup(IAccountRepository accountRepository)
        {            
            this.AddAccount(accountRepository, "admin", "admin");
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
