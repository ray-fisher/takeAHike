using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace takeAHike.Models.Users
{
    public interface IUserRepository
    {
        //   c r e a t e

        public User Create(User u);


        //   r e a d

        public IQueryable<User> GetAllUsers();
        public User GetUserByEmailAddress(string emailAddress);
        public User GetUserById(int id);
        public bool Login(User u);
        public void Logout();
        public bool IsUserLoggedIn();
        public int GetLoggedInUserId();
        public string GetLoggedInUserName();


        //   u p d a t e

        public bool ChangePassword(string oldPassword, string newPassword);

        public User Update(User u);


        //   d e l e t e

        public bool Delete(int id);

        public bool Delete(User u);
    }
}
