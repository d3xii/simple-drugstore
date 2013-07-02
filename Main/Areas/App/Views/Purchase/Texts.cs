using SDM.Core.Localization;

namespace SDM.Main.Areas.App.Views.Purchase
{
    public class Texts : CustomLocalizationScopeBase
    {
        public string PurchasesManagement = "Purchases";
        public string AddTransaction = "Add Transaction";
        public string EditTransaction = "Edit Transaction";
        public string HeaderInformation = "Header Information";
        public string DetailInformation = "Detail Information";
        public string QuickAdd = "Quick Add:";
        public string ValidationItemNameMustNotBeEmpty = "The item name must not be empty.";
        public string ValidationQuantityMustBeGreaterThanZero = "The quantity must be greater than zero.";
    }
}