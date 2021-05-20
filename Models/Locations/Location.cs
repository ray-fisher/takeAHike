using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using takeAHike.Models.Users;

namespace takeAHike.Models.Locations
{
    public class Location
    {
        // p r o p e r t i e s 
        [HiddenInput(DisplayValue = false)]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int locationId { get; set; }
        public User User { get; set; }
        [HiddenInput(DisplayValue = false)]
        //[ForeignKey ("userId")]
        public int UserId { get; set; }
        public string locationName { get; set; } 
        public int distance { get; set; }
        public int time { get; set; }

    }
}
