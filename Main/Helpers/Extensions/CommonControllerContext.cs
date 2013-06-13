using System;
using System.Web;
using SDM.Domain.Models;
using SDM.Infrastructure.Database;

namespace SDM.Main.Helpers.Extensions
{
    /// <summary>
    /// Contains context of the controller and supports <see cref="IDisposable"/> interface.
    /// This class should be created by <see cref="ControllerExtensions.GetContext"/> method
    /// and put into using { } block so no variable declaration is needed.
    /// </summary>
    public class CommonControllerContext : IDisposable
    {
        //**************************************************
        //
        // Public variables
        //
        //**************************************************

        #region Public variables

        /// <summary>
        /// Gets http context.
        /// </summary>
        public HttpContextBase HttpContext { get; internal set; }

        /// <summary>
        /// Gets database context.
        /// </summary>
        public DatabaseContext Database { get; internal set; }

        #endregion


        //**************************************************
        //
        // Lazy-loading properties
        //
        //**************************************************

        #region Lazy-loading properties

        /// <summary>
        /// Gets current logged in account.
        /// </summary>
        private AccountModel _currentAccount;

        /// <summary>
        /// Gets current logged in account.
        /// This is a lazy-loading property.
        /// </summary>
        public AccountModel CurrentAccount
        {
            get
            {
                return _currentAccount ?? (_currentAccount = this.GetCurrentAccount());
            }
        }

        #endregion



        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            if (this.Database != null)
            {
                this.Database.Dispose();
            }
        }

        #endregion


        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods

        /// <summary>
        /// Gets currently logged in account.
        /// </summary>
        public AccountModel GetCurrentAccount()
        {
            // get account name
            string name = HttpContext.User.Identity.Name;

            // search the name
            AccountModel accountModel = this.Database.Accounts.GetByName(name);

            // return result
            return accountModel;
        }

        #endregion

    }
}