using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace takeAHike.Models.Users
{
    public class User
    {
        [HiddenInput(DisplayValue = false)]
        // [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool? IsAdmin { get; set; }
    }
}
