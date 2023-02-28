using CI_PLATFOEM_REPOSITORY.Repository;
using CI_PLATFORM.Models;
using CI_PLATFORM_REPOSITORY.DataDB;
using Microsoft.AspNetCore.Mvc;

namespace CI_PLATFORM.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly IRepository<User> _userRepository;
        public UserAuthenticationController(IRepository<User> repository)
        {
            _userRepository = repository;
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
                var userExists = _userRepository.ExistsAsync(loginobj.Email);
                

                if (await userExists)
                {
                    return View(loginobj);
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

        public IActionResult ForgotPassword(ForgotPassword forgotpasswordobj)
        {
            //if (ModelState.IsValid)
            //{


            //    //if ()
            //    //{
            //    //    ModelState.AddModelError("Email", "Enter valid Email address..!");
            //    //    return View(forgotpasswordobj);
            //    //}
            //    //else
            //    //{
            //    //    return RedirectToAction("ResetPassword", "UserAuthentication");
            //    //}

            //}
            //else
            //{
            //    return View();
            //}
            return View();
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
                //if(registrationobj.Password != registrationobj.ConfirmPassword)
                //{
                //    ModelState.AddModelError("ConfirmPassword", "Password not matching..!");
                //    return View(registrationobj);
                //}

                CI_PLATFORM_REPOSITORY.DataDB.User user = new User()
                {
                    FirstName = registrationobj.FirstName,
                    LastName = registrationobj.LastName,
                    PhoneNumber = registrationobj.PhoneNumber,
                    Email = registrationobj.Email,
                    Password = registrationobj.Password
                };

                _userRepository.AddAsync(user);
                _userRepository.SaveChangesAsync(user);
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
