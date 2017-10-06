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
            string query2 = $"SELECT id FROM user WHERE Email = '{model.Email}';";
            List<Dictionary<string,object>> EmailCheck = DbConnector.Query(query2);
            if(EmailCheck.Count > 1) {
                ModelState.AddModelError("Email", "Email already in use");
            }
            if(ModelState.IsValid) {
                string query = $"INSERT INTO user (FirstName, LastName, Email, Password, Confirm, CreatedAt, UpdatedAt) VALUES('{model.FirstName}', '{model.LastName}', '{model.Email}', '{model.Password}', '{model.Confirm}', NOW(), NOW())";
                DbConnector.Execute(query);
                string query3 = $"SELECT id FROM user WHERE Email = '{model.Email}';"; 
                List<Dictionary<string,object>> CurrentUser = DbConnector.Query(query3);
                HttpContext.Session.SetInt32("id", (int)CurrentUser[0]["id"]);
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
                string query = $"SELECT * FROM user WHERE Email = '{Email}';";
                List<Dictionary<string,object>> CurrentUser = DbConnector.Query(query);
                if(CurrentUser != null) {
                    if((string)CurrentUser[0]["Password"] == Password) {
                        return RedirectToAction("Success");
                    }
                }
            }
            ViewBag.Error = "Invalid Email or Password";
            return View();
        }

        [HttpGet]
        [Route("Success")]
        public IActionResult Success() {
            return View();
        }
    }
}
