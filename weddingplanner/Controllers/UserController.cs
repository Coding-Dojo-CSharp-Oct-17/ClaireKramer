using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using weddingplanner.Models;
using System.Linq;

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
            if(ModelState.IsValid) {
                _context.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Dashboard", "Wedding");
            }
            else {
                ViewBag.errors = ModelState.Values;
                return View("Index");
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string Email, string Password) {
            User CheckEmail = _context.Users.SingleOrDefault(user => user.Email == Email);
            if(CheckEmail != null) {
                if(Password == CheckEmail.Password) {
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
            return View("Index");
        }

    }
}
