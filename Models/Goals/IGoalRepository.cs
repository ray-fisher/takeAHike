using System.Linq;

namespace takeAHike.Models.Goals
{
    public interface IGoalRepository
    {
        // c r e a t e
        public Goal AddGoal(Goal g);

        // r e a d
        public IQueryable<Goal> GetAllGoals(int UserId);
        public Goal GetGoalById(int id);

        // u p d a t e 
        public Goal EditGoal(Goal g);

        // d e l e t e
        public bool DeleteGoal(int id);
    }
}
