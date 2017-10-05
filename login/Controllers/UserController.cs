using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using login.Models;
using DbConnection;

namespace login.Controllers
{
    public class UserController : Controller
    {
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
                string query = $"INSERT INTO user (FirstName, LastName, Email, Password, Confirm, CreatedAt, UpdatedAt) VALUES('{model.FirstName}', '{model.LastName}', '{model.Email}', '{model.Password}', '{model.Confirm}', NOW(), NOW())";
                DbConnector.Execute(query);
                return RedirectToAction("Success");
            }
            else {
                ViewBag.errors = ModelState.Values;
                return View();
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string Email, string Password) {
            if(Email != null) {
                string query = $"SELECT * FROM user WHERE Email = '{Email}'";
                var CurrentUser = DbConnector.Query(query);
                if(CurrentUser != null) {
                    Console.WriteLine(CurrentUser);
                    return RedirectToAction("Success");
                }
            }
            ViewBag.Errors = new List<string>{
                "Invalid Email or Password"
            };
            return View();
        }

        [HttpGet]
        [Route("Success")]
        public IActionResult Success() {
            return View();
        }
    }
}
