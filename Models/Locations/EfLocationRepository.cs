using System.Linq;
using takeAHike.Models.Users;

namespace takeAHike.Models.Locations
{
    public class EfLocationRepository
        : ILocationRepository
    {
        // f i e l d s
        private AppDbContext _context;
        private IUserRepository _userRepository;

        // c o n s t r u c t o r s
        public EfLocationRepository(AppDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }
           
        // m e t h o d s
        // c r e a t e 
        public Location AddLocation(Location l)
        {
            if (_userRepository.IsUserLoggedIn())
            {
                l.UserId = _userRepository.GetLoggedInUserId();
                _context.Locations.Add(l);
                _context.SaveChanges();
                return l;
            }
            return null;
        }
        // r e a d 
        public IQueryable<Location> GetAllLocations(int UserId)
        {
            if (_userRepository.IsUserLoggedIn())
            {
                return _context.Locations.Where(l => l.UserId == _userRepository.GetLoggedInUserId());
            }
            Location[] noLocation = new Location[0];
            return noLocation.AsQueryable();
            
            // return _context.Locations;
        }

        public Location GetLocationById(int id)
        {
            return _context.Locations.FirstOrDefault(l => l.locationId == id && l.UserId == _userRepository.GetLoggedInUserId());
        }

        // u p d a t e 
        public Location EditLocation(Location l)
        {
            Location locationToUpdate = GetLocationById(l.locationId);
            if (locationToUpdate != null)
            {
                locationToUpdate.locationName = l.locationName;
                locationToUpdate.distance = l.distance;
                _context.SaveChanges();
            }
            return locationToUpdate;
        }
        // d e l e t e
        public bool DeleteLocation(int id)
        {
            Location locationToDelete = GetLocationById(id);
            if (locationToDelete == null)
            {
                return false;
            }
            _context.Locations.Remove(locationToDelete);
            _context.SaveChanges();
            return true;
        }

        //public IQueryable<Location> GetAllLocations()
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}
