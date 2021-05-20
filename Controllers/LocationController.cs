using Microsoft.AspNetCore.Mvc;
using takeAHike.Models.Locations;
using takeAHike.Models.Users;

namespace takeAHike.Controllers
{
    public class LocationController 
        : Controller
    {
        // f i e l d s  &  p r o p e r t i e s 
        private ILocationRepository _repository;
        private IUserRepository _userRepository;

        // c o n s t r u c t o r s 
        public LocationController(ILocationRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        // m e t h o d s //
        // c r e a t e
        [HttpGet]
        public IActionResult AddLocation()
        {
            return View(
                new Location
                {
                    UserId = _userRepository.GetLoggedInUserId()
                }
                );
        }

        [HttpPost]
        public IActionResult AddLocation(Location l)
        {
            if (ModelState.IsValid)
            {
                _repository.AddLocation(l);
                return RedirectToAction("DetailLocation", new { id = l.locationId });
            }
            return View(l);
        }

        // r e a d
        public IActionResult Index()
        {
            return View(_repository.GetAllLocations(_userRepository.GetLoggedInUserId()));
        }
        public IActionResult DetailLocation(int id)
        {
            Location l = _repository.GetLocationById(id);
            if (l == null)
            {
                return RedirectToAction("Index", "Location");
            }
            return View(l);
        }
        // u p d a t e 
        [HttpGet]
        public IActionResult EditLocation(int id)
        {
            Location l = _repository.GetLocationById(id);
            if (l == null)
            {
                return RedirectToAction("Index");
            }
            return View(l);
        }
        [HttpPost]
        public IActionResult EditLocation(Location l)
        {
            if (ModelState.IsValid)
            {
                _repository.EditLocation(l);
                return RedirectToAction("DetailLocation", new { id = l.locationId });
            }
            return View(l);
        }

        // d e l e t e

        [HttpGet]
        public IActionResult DeleteLocation(int id)
        {
            Location l = _repository.GetLocationById(id);
            if (l == null)
            {
                return RedirectToAction("Location");
            }
            return View(l);
        }

        [HttpPost]
        public IActionResult DeleteLocation(Location l)
        {
            _repository.DeleteLocation(l.locationId);
            return RedirectToAction("Location");
        }

    }
}
