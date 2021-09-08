
using HotelAbshar.Application.DTOs.UserViewModels;
using HotelAbshar.Application.Interfaces;
using HotelAbshar.Common.Convertors;
using HotelAbshar.Common.Generators;
using HotelAbshar.Common.Security;
using HotelAbshar.Common.Senders;
using HotelAbshar.Domain.Entities.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    public class AccountController : Controller
    {
        private readonly IViewRenderService _viewRenderService;
        private readonly IUserService _userService; 
        public AccountController(IUserService userService,IViewRenderService viewRenderService)
        {
            _viewRenderService = viewRenderService;
            _userService = userService;
        }
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.NoIsValid = true;
                return View(register);
            }
            if (register.Password != register.RePassword)
            {
                ViewBag.WrongRePassword = true;
                return View(register);
            }

            if (_userService.UserNameExist(register.UserName))
            {
                ViewBag.ExistUserName = true;
                return View(register);
            }
            if (_userService.EmailExist(register.Email))
            {
                ViewBag.ExistUEmail = true;
                return View(register);
            }


            User user = new User
            {
                IsActive = false,
                Email = FixText.FixEmail(register.Email),
                FullName = register.FullName,
                UserName = register.UserName,
                Password = MyPasswordHasher.EncodePasswordMd5(register.Password),
                ActivationCode = GeneratCode.GenerateUniqCode(),
                

            };
            _userService.RegisterUser(user);

            #region SendEmail
            var body = _viewRenderService.RenderToStringAsync("_ActivateEmail", user);
            EmailSender.Send(user.Email, "فعالسازی حساب کاربری", body);
            #endregion
            return View("SuccessRegister");
        }
        #region ActiveUser
        public IActionResult ActiveUser(string id)
        {
            ViewBag.IsActive = _userService.ActivateUser(id);
            return View();
        }
        #endregion


        #region ForgotPassword
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPasswordViewModel forgot)
        {
            if (ModelState.IsValid==false)
            {
                ViewBag.NoIsValid = true;
                return View(forgot);
            }
            var user = _userService.GetUserByEmail(forgot.Email);
            if (user == null)
            {
                ViewBag.UserNull = true;
                return View(forgot);
            }
            else
            {
                var body = _viewRenderService.RenderToStringAsync("_ForgotPassword", user);
                EmailSender.Send(user.Email, "تغییر کلمه عبور", body);
                return View("ResetPasswordEmail", user);

            }
        }


        public IActionResult ResetPassword(string id)
        {
            var user = _userService.GetUserByActivationCode(id);
            if (user == null)
            {
                return BadRequest();
            }
            //  ViewBag.activeCode = user.ActivationCode;

            return View(new ResetPasswordViewModel { ActivationCode = id });


        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel reset)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.NoIsValid = true;
                return View(reset);
            }
            if (reset.Password != reset.RePassword)
            {               
                ViewBag.WrongRePassword = true;
                return View(reset);
            }
            _userService.ResetPassword(reset.Password, reset.ActivationCode);
            return RedirectToAction("Login");
        }
        #endregion



        #region Login
        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.NoIsValid = true;
                return View(login);
            }
            var user = _userService.LoginUser(login.UserNameOrEmail, login.Password);
            if (user == null)
            {
                ViewBag.UserNull = true;
                return View(login);
            }

            else
            {
                if (user.IsActive)
                {
                    var Claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserID.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName),
                        new Claim(ClaimTypes.Email,user.Email),

                    };
                    var identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = true,

                    };
                    HttpContext.SignInAsync(principal, properties);
                    ViewBag.IsSeccess = true;
                    return View();
                }
                else
                {
                    ViewBag.NoActive = true;
                    return View(login);
                }
            }

        }
        #endregion

        #region LogOut
        [Route("LogOut")]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }
        #endregion
    }
}
