using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using takeAHike.Models.Users;

namespace takeAHike.Models.Goals
{
    public class Goal
    {
        // p r o p e r t i e s\
        [HiddenInput(DisplayValue = false)]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int goalId { get; set; }

        public User User { get; set; }

        [HiddenInput(DisplayValue = false)]
        //[ForeignKey ("userId")]
        public int UserId { get; set; }

        //[ForeignKey ("locationId")]
        public int locationId { get; set; }

        public string locationName { get; set; }
        public string distance { get; set; }
        public bool complete { get; set; }

        // c o n s t r u c t o r s

        // m e t h o d s

    }
}
