﻿using SDM.Localization.Core;
using SDM.Main.Views.Home;

namespace SDM.Main.Areas.Admin.Controllers
{
    public class AdminHomeControllerTexts : LocalizationScopeBase
    {
        public string InvalidPassword = "Invalid password.";
        public string Mandatory = "You must enter the password.";
        public string ValidDatabaseConnection = "Connected to database successfully.";
        public string InvalidDatabaseConnection = "Unable to connect to the database server. Error: \r\n{0}";
    }
}