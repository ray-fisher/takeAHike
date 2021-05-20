using System.Linq;
using takeAHike.Models.Users;

namespace takeAHike.Models.Goals
{
    public class EfGoalRepository
        : IGoalRepository
    {
        // f i e l d s 
        private AppDbContext _context;
        private IUserRepository _userRepository;

        // c o n s t r u c t o r s
        public EfGoalRepository(AppDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }


        // m e t h o d s 
        // c r e a t e 

        public Goal AddGoal(Goal g)
        {
            if (_userRepository.IsUserLoggedIn())
            {
                g.UserId = _userRepository.GetLoggedInUserId();
                _context.Goals.Add(g);
                _context.SaveChanges();
                return g;
            }
            return null;
        }
        // r e a d 
        public IQueryable<Goal> GetAllGoals(int UserId)
        {
            if (_userRepository.IsUserLoggedIn())
            {
                return _context.Goals.Where(g => g.UserId == _userRepository.GetLoggedInUserId());
            }
            Goal[] noGoals = new Goal[0];
            return noGoals.AsQueryable();

        }

        public Goal GetGoalById(int id)
        {
            return _context.Goals.FirstOrDefault(g => g.goalId == id && g.UserId == _userRepository.GetLoggedInUserId());
        }

        // u p d a t e 
        public Goal EditGoal(Goal g)
        {
            Goal goalToUpdate = GetGoalById(g.goalId);
            if (goalToUpdate != null)
            {
                goalToUpdate.locationName = g.locationName;
                goalToUpdate.distance = g.distance;
                goalToUpdate.complete = g.complete;
                _context.SaveChanges();
            }
            return goalToUpdate;
        }
        // d e l e t e
        public bool DeleteGoal(int id)
        {
            Goal goalToDelete = GetGoalById(id);
            if (goalToDelete == null)
            {
                return false;
            }

            _context.Goals.Remove(goalToDelete);
            _context.SaveChanges();
            return true;
        }

    }
}
