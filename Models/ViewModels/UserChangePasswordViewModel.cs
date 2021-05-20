using System.ComponentModel.DataAnnotations;

namespace takeAHike.Models.ViewModels
{
    public class UserChangePasswordViewModel
    {
        [Required(ErrorMessage = "Current Password Is Required")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New Password Is Required")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "The Passwords Must Match")]
        [Required(ErrorMessage = "Verification Password Is Required")]
        public string VerifyNewPassword { get; set; }
    }
}
