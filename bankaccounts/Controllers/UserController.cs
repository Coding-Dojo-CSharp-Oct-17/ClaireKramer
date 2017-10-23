using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using bankaccounts.Models;
using System.Linq;

namespace bankaccounts.Controllers
{
    public class UserController : Controller
    {
        private Context _context;
        public UserController(Context context) {
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(User model) {
            if(ModelState.IsValid) {
                User NewUser = new User {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    Balance = 0
                };
                _context.Add(NewUser);
                _context.SaveChanges();
                ViewBag.message = "Successfully Registered. You may now login!";
                return View("Login");
            }
            else {
                ViewBag.errors = ModelState.Values;
                return View("Index");
            }
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        [Route("ValidLogin")]
        public IActionResult ValidLogin(User model) {
            User CheckEmail = _context.Users.SingleOrDefault(user => user.Email == model.Email);
            if(CheckEmail != null) {
                if(model.Password == CheckEmail.Password) {
                    ViewBag.User = CheckEmail;
                    HttpContext.Session.SetInt32("UserId", CheckEmail.Id);
                    int? user_id = CheckEmail.Id;
                    return Redirect($"/account/{user_id}");
                }
                else {
                    ViewBag.errors = "Incorrect Password";
                    return View("Login");
                }
            }
            else {
                ViewBag.errors = "Email not registered";
                return View("Login");
            }
        }

        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout() {
            HttpContext.Session.Clear();
            return View("Login");
        }
    }
}
