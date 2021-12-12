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
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheaplayMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly CheaplayContext _db;
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
                //var encPass = EncryptPassword(model.Password);
                //User user = await _db.Users.FirstOrDefaultAsync(u => u.Login == model.Login && u.Password.Equals(encPass));
                User user = await _db.Users.FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user.Login, user.Role);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incorrect login or password");
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

                    await Authenticate(model.Login, Roles.User.ToString());

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Incorrect login or password");
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
        public async Task<ActionResult> Profile()
        {
            var login = HttpContext.User.Identity.Name;
            User user = await _db.Users.FirstOrDefaultAsync(u => u.Login.Equals(login));
            return View(user);
        }

        public async Task<ActionResult> Update(User userUpdate)
        {
            var login = HttpContext.User.Identity.Name;
            User user = await _db.Users.FirstOrDefaultAsync(u => u.Login.Equals(login));
            StringBuilder updateProblems = new StringBuilder("There are some problems:\n");
            if (userUpdate.FirstName != null && userUpdate.FirstName != "")
            {
                user.FirstName = userUpdate.FirstName;
            }
            if (userUpdate.SecondName != null && userUpdate.SecondName != "")
            {
                user.SecondName = userUpdate.SecondName;
            }
            if (userUpdate.Email != null && userUpdate.Email != "")
            {
                if (CheckEmail(userUpdate.Email))
                {
                    user.Email = userUpdate.Email;
                }
                else
                {
                    updateProblems.Append("- Email should be like smth@xxx.com\n");
                    updateProblems.Append("- This email probably already uses\n");
                }
            }
            if (userUpdate.Birthday != new DateTime())
            {
                if (CheckBirthday(userUpdate.Birthday))
                {
                    user.Birthday = userUpdate.Birthday;
                }
                else
                {
                    updateProblems.Append("- Your birthday cannot be today or in future :D\n");
                }
            }
            if (userUpdate.Password != null && userUpdate.Password != "")
            {
                if (CheckPassword(userUpdate.Password))
                {
                    user.Password = userUpdate.Password;
                }
                else
                {
                    updateProblems.Append("- Password should be 8 or more characters\n");
                }
            }

            if (updateProblems.Length > 25)
            {
                ViewData["UpdateResult"] = updateProblems.ToString();                
            }
            else
            {
                await _db.SaveChangesAsync();
                ViewData["UpdateResult"] = "Your account has been successfully updated!";
            }
            return View();
        }

        public bool CheckBirthday(DateTime date)
        {
            return date < DateTime.Now;
        }

        public bool CheckPassword(string pass)
        {
            return pass!=null && pass.Length > 7;
        }

        public string EncryptPassword(string password)
        {
            return password.GetHashCode().ToString() + "Eleks".GetHashCode();
        }

        public bool CheckEmail(string email)
        {
            if (_db.Users.Any(u => u.Email.Equals(email)))
            {
                return false;
            }
            var emailCheckup = new EmailAddressAttribute();
            if (!(emailCheckup.IsValid(email)))
                return false;
            return true;
        }
    }
}
