using CheaplayMVC.Data;
using CheaplayMVC.Models;
using CheaplayMVC.Models.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace CheaplayMVC.Controllers
{
    public class UserController : Controller
    {
        private CheaplayContext _db;
        public UserController(CheaplayContext context)
        {
            _db = context;
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _db.Users.FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user.Login, user.Role); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {                
                User user = await _db.Users.FirstOrDefaultAsync(u => u.Login == model.Login);
                if (user == null)
                {
                    _db.Users.Add(new User
                    {
                        FirstName = model.FirstName,
                        SecondName = model.SecondName,
                        Login = model.Login,
                        Email = model.Email,
                        Password = model.Password,
                        Birthday = model.Birthday,
                        Role = Roles.User.ToString()
                    });
                    await _db.SaveChangesAsync();

                    await Authenticate(model.Login, Roles.User.ToString()); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
           
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }

        [Authorize(Roles = "Admin, User")]
        public ActionResult Profile()
        {
            return View();
        }
    }
}
