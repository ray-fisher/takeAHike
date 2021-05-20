using System.Linq;

namespace takeAHike.Models.Locations
{
    public interface ILocationRepository
    {
        // c r e a t e
        public Location AddLocation(Location l);
        // r e a d 
        public IQueryable<Location> GetAllLocations(int UserId);
        public Location GetLocationById(int id);

        // u p d a t e 
        public Location EditLocation(Location l);

        // d e l e t e
        public bool DeleteLocation(int id);
    }
}
