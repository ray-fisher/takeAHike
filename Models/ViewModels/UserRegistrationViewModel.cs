using System.ComponentModel.DataAnnotations;

namespace takeAHike.Models.ViewModels
{
    public class UserRegistrationViewModel
    {
        [Required(ErrorMessage = "UserName Is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The Passwords Must Match")]
        [Required(ErrorMessage = "Verification Password Is Required")]
        public string VerificationPassword { get; set; }
    }
}
