using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Cryptography;
using SDM.Core.Localization;
using SDM.Domain.Models.Base;
using SDM.Localization.Core;

namespace SDM.Domain.Models
{
    /// <summary>
    /// Contains information of an account in the database.
    /// </summary>
    [Table("Account")]
    public class AccountModel : ModelBase, ILocalizable<AccountModel.Texts>
    {
        //**************************************************
        //
        // Public properties
        //
        //**************************************************

        #region Public properties

        /// <summary>
        /// Gets or sets user name of the account.
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets in-memory password.
        /// </summary>
        private string _password;

        /// <summary>
        /// Gets or sets in-memory password.
        /// </summary>
        [NotMapped, Required]
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                this.EncryptedPassword = Encrypt(value);
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
            if (Encrypt(currentPassword) != this.EncryptedPassword)
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
        /// Validates this model as newly created one.
        /// This checks for existing account name.
        /// </summary>
        public string ValidateToCreate(string password2, IDbSet<AccountModel> accounts)
        {
            // not duplicated name
            if (accounts.IsExisted(this.UserName))
            {
                // duplicated
                return this.Localize(t => t.DuplicatedUserName);
            }

            // compare with confirm password
            if (this.Password != password2)
            {
                return this.Localize(t => t.NewPasswordMustBeTheSame);
            }

            // ok
            return null;
        }

        #endregion


        //**************************************************
        //
        // Public static methods
        //
        //**************************************************

        #region Public static methods

        /// <summary>
        /// Gets encrypted BASE64 string given text.
        /// </summary>        
        public static string Encrypt(string text)
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

        public class Texts : CustomLocalizationScopeBase
        {
            public string UserName = "User Name";
            public string Password = "Password";
            public string IsAdmin = "Is administrator?";

            public string CurrentPasswordNotMatched = "The current password you entered does not match saved password in database.";
            public string NewPasswordMustBeTheSame = "New password must be the same.";
            public string DuplicatedUserName = "The user name has been used. Please choose another one.";
        }

        #endregion
    }
}