using Microsoft.EntityFrameworkCore;
using takeAHike.Models.Goals;
using takeAHike.Models.Locations;
using takeAHike.Models.Users;

namespace takeAHike.Models
{
    public class AppDbContext
        :DbContext
    {
        // f i e l d s
        public DbSet<User> Users { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Location> Locations { get; set; }


        // c o n s t u c t o r s 
        public AppDbContext(DbContextOptions<AppDbContext> options)
             : base(options)
        {
        }
        // m e t h o d s
    }
}
