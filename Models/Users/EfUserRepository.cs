using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace takeAHike.Models.Users
{
    public class EfUserRepository
        : IUserRepository
    {
        //   f i e l d s   &   p r o p e r t i e s

        private AppDbContext _context;
        private ISession _session;


        //   c o n s t r u c t o r s

        public EfUserRepository(AppDbContext context, IHttpContextAccessor httpContext) // , ISession session)
        {
            _context = context;
            _session = httpContext.HttpContext.Session;
        }


        //   m e t h o d s

        //   c r e a t e

        public User Create(User u)
        {
            string encryptedPassword = EncryptPassword(u.Password);
            u.Password = encryptedPassword;

            User existingUser = GetUserByEmailAddress(u.Username);
            if (existingUser != null)
            {
                return null;
            }

            try
            {
                _context.Users.Add(u);
                _context.SaveChanges();
                return u;
            }
            catch (Exception e)
            {
            }

            return null;
        }


        //   R e a d

        public IQueryable<User> GetAllUsers()
        {
            return _context.Users;
        }

        public User GetUserByEmailAddress(string emailAddress)
        {
            return _context.Users.FirstOrDefault(u => u.Username == emailAddress);
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public bool Login(User user)
        {
            string encPassword = EncryptPassword(user.Password);

            User existingUser = _context.Users.FirstOrDefault
               (u => u.Username == user.Username && u.Password == encPassword);

            if (existingUser == null || existingUser.Password != encPassword)
            {
                return false;
            }
            else
            {
                _session.SetInt32("userid", existingUser.Id);
                _session.SetString("username", user.Username);
                return true;
            }
        } 

        public void Logout()
        {
            _session.Remove("userid");
            _session.Remove("username");
        }

        public bool IsUserLoggedIn()
        {
            int? userId = _session.GetInt32("userid");
            if (userId == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int GetLoggedInUserId()
        {
            int? userId = _session.GetInt32("userid");
            if (userId == null)
            {
                return -1;
            }
            else
            {
                return userId.Value;
            }
        }

        public string GetLoggedInUserName()
        {
            return _session.GetString("username");
        }


        //   U p d a t e

        public bool ChangePassword(string oldPassword, string newPassword)
        {
            if (!IsUserLoggedIn())
            {
                return false;
            }

            User userToUpdate = GetUserById(GetLoggedInUserId());
            if (userToUpdate != null && userToUpdate.Password == oldPassword)
            {
                userToUpdate.Password = newPassword;
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public User Update(User u)
        {
            User userToUpdate = GetUserById(u.Id);
            if (userToUpdate != null)
            {
                userToUpdate.IsAdmin = u.IsAdmin;
                userToUpdate.Password = u.Password;
                _context.SaveChanges();
            }
            return userToUpdate;
        }


        //   d e l e t e

        public bool Delete(int id)
        {
            User userToDelete = GetUserById(id);
            if (userToDelete == null)
            {
                return false;
            }
            _context.Users.Remove(userToDelete);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(User u)
        {
            return Delete(u.Id);
        }


        //   p r i v a t e   m e t h o d s

        private string EncryptPassword(string password)
        {
            //   SHA - Secure Hash Algorithm
            SHA256 hashAlgorithm = SHA256.Create();

            byte[] passwordArray = Encoding.ASCII.GetBytes(password);
           

            byte[] encryptedPasswordArray =
               hashAlgorithm.ComputeHash(passwordArray);

            string result = BitConverter.ToString(encryptedPasswordArray);

            result = result.Replace("-", "");

            return result;
        }

        private string GenerateRandomPassword(int length = 8)
        {
            Random r = new Random();

            string result = "";
            for (int i = 0; i < length; i++)
            {
                result = result + (char)r.Next(33, 126);
            }

            return result;
        }
    }
}
