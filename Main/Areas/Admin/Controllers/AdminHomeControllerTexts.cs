using SDM.Localization.Core;

namespace SDM.Main.Areas.Admin.Controllers
{
    public class AdminHomeControllerTexts : LocalizationScopeBase
    {
        public string InvalidPassword = "Invalid password.";
        public string Mandatory = "You must enter the password.";
        public string ValidDatabaseConnection = "Database connection is valid.";
        public string InvalidDatabaseConnection = "Unable to connect to the database server. Error: {0}";
    }
}