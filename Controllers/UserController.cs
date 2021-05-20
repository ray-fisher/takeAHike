using Microsoft.AspNetCore.Mvc;
using takeAHike.Models.Users;

namespace takeAHike.Controllers
{
    public class UserController : Controller
    {
        //   f i e l d s   &   p r o p e r t i e s

        private IUserRepository _repository;


        //   c o n s t r u c t o r s

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }


        //   m e t h o d s
        //   c r e a t e
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(takeAHike.Models.ViewModels.UserRegistrationViewModel ur)
        {
            if (ModelState.IsValid)
            {
                User u = new User();
                u.IsAdmin = false;
                u.Password = ur.Password;
                u.Username = ur.UserName;
                User newUser = _repository.Create(u);
                if (newUser == null)
                {
                    ModelState.AddModelError("", "This User Already Exists.");
                    return View(ur);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(ur);
        }


        //   r e a d
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User u)
        {
            bool loggedIn = _repository.Login(u);
            if (loggedIn == true && ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(u);
        }

        public IActionResult Logout()
        {
            _repository.Logout();
            return RedirectToAction("Index", "Home");
        }


        //   u p d a t e
        [HttpGet]
        public IActionResult ChangePassword()
        {
            User u = _repository.GetUserById(_repository.GetLoggedInUserId());
            if(u != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Register");
            }
        }

        [HttpPost]
        public IActionResult ChangePassword(takeAHike.Models.ViewModels.UserChangePasswordViewModel cp)
        {
            if (ModelState.IsValid)
            {
                bool success =
                   _repository.ChangePassword(cp.CurrentPassword, cp.NewPassword);
                if (success == true)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Unable To Change Password");
                return View(cp);
            }
            return View(cp);
        }


        //   d e l e t e
    }


}
