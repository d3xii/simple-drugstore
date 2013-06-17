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
        [NotMapped]
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;

                // encrypt if have value
                if (!string.IsNullOrEmpty(value))
                {
                    this.EncryptedPassword = Encrypt(value);
                }                               
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
        // Constructors
        //
        //**************************************************

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public AccountModel()
        {
            this.IsAdmin = true;
            this.IsEnabled = true;
        }

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
        /// Creates this model as newly created one and add to the data source.
        /// This checks for existing account name.        
        /// </summary>
        public string Create(string password2, IDbSet<AccountModel> accounts)
        {
            // not duplicated name
            if (accounts.IsExisted(this.UserName))
            {
                // duplicated
                return this.Localize(t => t.DuplicatedUserName);
            }

            // must have password
            if (string.IsNullOrEmpty(this.Password))
            {
                return this.Localize(t => t.MustEnterPassword);
            }

            // compare with confirm password
            if (this.Password != password2)
            {
                return this.Localize(t => t.NewPasswordMustBeTheSame);
            }

            // add to data source
            accounts.Add(this);

            // ok
            return null;
        }

        /// <summary>
        /// Updates this model with information contained in the dummy model.
        /// </summary>
        public string Update(AccountModel model, string password2)
        {
            // compare with confirm password
            if (!string.IsNullOrEmpty(model.Password))
            {
                if (model.Password != password2)
                {
                    return this.Localize(t => t.NewPasswordMustBeTheSame);
                }

                // update password
                this.Password = model.Password;
            }            

            // update other information
            this.IsAdmin = model.IsAdmin;
            this.IsEnabled = model.IsEnabled;

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
            public string IsEnabled = "Is enabled?";

            public string CurrentPasswordNotMatched = "The current password you entered does not match saved password in database.";
            public string NewPasswordMustBeTheSame = "New password must be the same.";
            public string DuplicatedUserName = "The user name has been used. Please choose another one.";
            public string MustEnterPassword = "You must enter password.";
        }

        #endregion
    }
}