using Microsoft.AspNetCore.Mvc;
using takeAHike.Models.Goals;
using takeAHike.Models.Users;

namespace takeAHike.Controllers
{
    public class GoalController
        : Controller
    {
        // f i e l d s  &  p r o p e r t i e s 
        private IGoalRepository _repository;
        private IUserRepository _userRepository;

        // c o n s t r u c t o r s 
        public GoalController(IGoalRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        // m e t h o d s //
        // c r e a t e
        [HttpGet]
        public IActionResult AddGoal()
        {
            return View(
                new Goal
                {
                    UserId = _userRepository.GetLoggedInUserId()
                }
                );
        }

        [HttpPost]
        public IActionResult AddGoal(Goal g)
        {
            if (ModelState.IsValid)
            {
                _repository.AddGoal(g);
                return RedirectToAction("DetailGoal", new { id = g.goalId });
            }
            return View(g);
        }

        // r e a d
        public IActionResult Index()
        {
            return View(_repository.GetAllGoals(_userRepository.GetLoggedInUserId()));
        }

        public IActionResult DetailGoal(int id)
        {
            Goal g = _repository.GetGoalById(id);
            if (g == null)
            {
                return RedirectToAction("Index");
            }
            return View(g);
        }

        // u p d a t e 
        [HttpGet]
        public IActionResult EditGoal(int id)
        {
            Goal g = _repository.GetGoalById(id);
            if (g == null)
            {
                return RedirectToAction("Index");
            }
            return View(g);
        }
        [HttpPost]
        public IActionResult EditGoal(Goal g)
        {
            if (ModelState.IsValid)
            {
                _repository.EditGoal(g);
                return RedirectToAction("DetailGoal", new { id = g.goalId });

            }
            return View();
        }

        // d e l e t e
        [HttpGet]
        public IActionResult DeleteGoal(int id)
        {
            Goal g = _repository.GetGoalById(id);
            if (g == null)
            {
                return RedirectToAction("Index");
            }
            return View(g);
        }

        [HttpPost]
        public IActionResult DeleteGoal(Goal g)
        {
            // int goalId = g.goalId;
            _repository.DeleteGoal(g.goalId);
            return RedirectToAction("Index");
        }

    }
}
