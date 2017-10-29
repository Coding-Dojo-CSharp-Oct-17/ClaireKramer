using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using weddingplanner.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace weddingplanner.Controllers
{
    public class UserController : Controller
    {
        private WeddingContext _context;
        public UserController(WeddingContext context) {
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
            User CheckEmail = _context.Users.SingleOrDefault(user => user.Email == model.Email);
            if(CheckEmail != null) {
                ViewBag.errors = "Email already registered to an account";
                return View("Index");
            }
            if(ModelState.IsValid) {
                _context.Add(model);
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                model.Password = Hasher.HashPassword(model, model.Password);
                model.ConfirmPW = Hasher.HashPassword(model, model.ConfirmPW);
                _context.SaveChanges();
                ViewBag.errors = "Successfully Registered! You may now login!";
                return View("Index");
            }
            else {
                return View("Index");
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string Email, string Password) {
            User CheckEmail = _context.Users.SingleOrDefault(user => user.Email == Email);
            if(CheckEmail != null) {
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(CheckEmail, CheckEmail.Password, Password)) {
                    HttpContext.Session.SetInt32("UserId", CheckEmail.UserId);
                    return RedirectToAction("Dashboard", "Wedding");
                }
                else {
                    ViewBag.errors = "Incorrect Password";
                    return View("Index");
                }
            }
            else {
                ViewBag.errors = "Email not registered";
                return View("Index");
            }
        }

        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout() {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

    }
}
