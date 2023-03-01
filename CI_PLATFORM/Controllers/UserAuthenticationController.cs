using CI_PLATFOEM_REPOSITORY.Repository;
using CI_PLATFORM.Models;
using CI_PLATFORM_REPOSITORY.DataDB;
using Microsoft.AspNetCore.Mvc;

namespace CI_PLATFORM.Controllers
{
    public class UserAuthenticationController : Controller
    {
        
        private IRepository<User> db;

       
        public UserAuthenticationController(IRepository<User> _db)
        {
            db = _db;
        }

        public IActionResult Login(string? ReturnUrl="")
        {
            LoginViewModel loginobj = new LoginViewModel()
            {
                ReturnUrl = ReturnUrl
            };
            return View(loginobj);
        }
        [HttpPost]
        [Route("UserAuthentication/Login", Name = "UserLogin")]
        public async Task<IActionResult> Login(Models.LoginViewModel loginobj)
        {
            if (ModelState.IsValid)
            {
                
                var userExists = db.UserExistsAsync(loginobj.Email, loginobj.Password);
                

                if (await userExists)
                {
                    return RedirectToAction("MissionListing", "Mission"); ;
                }
                else
                {
                    ModelState.AddModelError("Password", "Invalid Credentials !!");
                    return View(loginobj);
                }
                 
                
             }
             else
            {
                return View();
            }
        }



        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [Route("UserAuthentication/ForgotPassword")]

        public async Task<IActionResult> ForgotPassword(ForgotPassword forgotpasswordobj)
        {
            if (ModelState.IsValid)
            {

                var userexists = db.ExistAsync(forgotpasswordobj.Email);
                if (await userexists)
                {
                    return RedirectToAction("ResetPassword", "UserAuthentication");
                   
                }
                else
                {
                    ModelState.AddModelError("Email", "Enter valid Email address..!");
                    return View(forgotpasswordobj);
                }

            }
            else
            {
                return View();
            }
            
        }


        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [Route("UserAuthentication/Registration", Name = "UserRegistration")]
        public IActionResult Registration(Models.Registration registrationobj)
        {
            if (ModelState.IsValid)
            {
                if (registrationobj.Password != registrationobj.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Password not matching..!");
                    return View(registrationobj);
                }

                CI_PLATFORM_REPOSITORY.DataDB.User user = new User()
                {
                    FirstName = registrationobj.FirstName,
                    LastName = registrationobj.LastName,
                    PhoneNumber = registrationobj.PhoneNumber,
                    Email = registrationobj.Email,
                    Password = registrationobj.Password
                };

                db.UserAddAsync(user);
                db.UserSaveChangesAsync(user);
                return View("Login");



            }
            else
            {
                return View();

            }
        
        }

        public IActionResult ResetPassword()
        {
            return View();
        }
    }
}
