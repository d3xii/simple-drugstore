using System.ComponentModel.DataAnnotations;

namespace SDM.Main.Views.Home
{
    public class HomeIndexViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsRememberMe { get; set; }

        public string LoginErrorMessage;
    }
}