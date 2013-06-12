using System;
using System.Security.Cryptography;
using SDM.Localization.Core;

namespace SDM.Domain.Models.Common
{
    /// <summary>
    /// Represents domain model: Common.Account.
    /// </summary>
    public class AccountModel : ILocalizable<AccountModel.Texts>
    {
        private string _password;
        //**************************************************
        //
        // Public variables
        //
        //**************************************************

        #region Public variables

        /// <summary>
        /// Gets or sets user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets in-memory password.
        /// </summary>
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                this.EncryptedPassword = this.Encrypt(value);
            }
        }

        /// <summary>
        /// Gets or sets encrypted password.
        /// </summary>
        public string EncryptedPassword { get; set; }

        /// <summary>
        /// Gets or sets a value indicates whether the account is enabled to use.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicates whether the account has admin permission.
        /// </summary>
        public bool IsAdmin { get; set; }

        #endregion


        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods

        /// <summary>
        /// Changes password of current account.
        /// </summary>
        public string ChangePassword(string currentPassword, string newPassword1, string newPassword2)
        {
            // check if current password matched
            if (this.Encrypt(currentPassword) != this.EncryptedPassword)
            {
                return this.Localize(t => t.CurrentPasswordNotMatched);
            }

            // check new passwords are matched
            if (newPassword1 != newPassword2)
            {
                return this.Localize(t => t.NewPasswordMustBeTheSame);
            }

            // ok, save it
            this.Password = newPassword1;

            // no error
            return null;
        }

        /// <summary>
        /// Gets encrypted BASE64 string given text.
        /// </summary>        
        public string Encrypt(string text)
        {
            // credit: Oli, http://stackoverflow.com/a/212526/633428
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(text);
            data = md5.ComputeHash(data);
            return Convert.ToBase64String(data);
        }

        #endregion


        //**************************************************
        //
        // Nested classes
        //
        //**************************************************

        #region Nested classes

        public class Texts : LocalizationScopeBase
        {
            public string CurrentPasswordNotMatched = "The current password you entered does not match saved password in database.";
            public string NewPasswordMustBeTheSame = "New password must be the same.";
        }

        #endregion

    }
}
