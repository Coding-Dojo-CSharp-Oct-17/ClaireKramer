using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using forms.Models;

namespace forms.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("submit")]
        public IActionResult Submit(string FirstName, string LastName, int Age, string Email, string Password) {
            Home NewUser = new Home {
                FirstName = FirstName,
                LastName = LastName,
                Age = Age,
                Email = Email, 
                Password = Password
            };
            TryValidateModel(NewUser);
            if(ModelState.IsValid) {
                return RedirectToAction("Success");
            }
            ViewBag.errors = ModelState.Values;
            return View();
        }

        [HttpGet]
        [Route("success")]
        public IActionResult Success() {
            return View();
        }
    }
}
