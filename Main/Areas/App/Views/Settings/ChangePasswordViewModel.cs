using System.ComponentModel.DataAnnotations;
using SDM.Main.Helpers.Extensions.CustomHtmlHelper;

namespace SDM.Main.Areas.App.Views.Settings
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string NewPassword2 { get; set; }

        public IHtmlMessage Message { get; set; }
    }
}