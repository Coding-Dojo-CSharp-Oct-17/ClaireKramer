using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DbConnection;
using wall.Models;
using wall.Controllers;

namespace wall.Controllers
{
    public class UserController : Controller
    {
        // GET: /Home/

        //NOTE(Devon):
        //Claire, very nice work overall!  see some of my comments for some ways to improve.
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(User model) {
            string query2 = $"SELECT id FROM users WHERE Email = '{model.Email}';";
            List<Dictionary<string,object>> EmailCheck = DbConnector.Query(query2);
            if(EmailCheck.Count > 1) {
                ModelState.AddModelError("Email", "Email already in use");
            }
            if(ModelState.IsValid) {
                string query = $"INSERT INTO users (first_name, last_name, email, password, created_at, updated_at) VALUES('{model.FirstName}', '{model.LastName}', '{model.Email}', '{model.Password}', NOW(), NOW())";
                DbConnector.Execute(query);
                string query3 = $"SELECT id, first_name FROM users WHERE Email = '{model.Email}';"; 
                List<Dictionary<string,object>> CurrentUser = DbConnector.Query(query3);
                HttpContext.Session.SetInt32("userId", (int)CurrentUser[0]["id"]);
                HttpContext.Session.SetString("name", (string)CurrentUser[0]["first_name"]);
                return RedirectToAction("Home", "Wall");
            }
            else {
                //NOTE(Devon):  Do you need to use ViewBag here?
                ViewBag.errors = ModelState.Values;
                return View();
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string Email, string Password) {
            if(Email != null) {
                string query = $"SELECT * FROM users WHERE email = '{Email}';";
                List<Dictionary<string,object>> CurrentUser = DbConnector.Query(query);
                if(CurrentUser != null) {
                    if((string)CurrentUser[0]["password"] == Password) {
                        HttpContext.Session.SetInt32("userId", (int)CurrentUser[0]["id"]);
                        HttpContext.Session.SetString("name", (string)CurrentUser[0]["first_name"]);
                        return RedirectToAction("Home", "Wall");
                    }
                }
            }
            ViewBag.Error = "Invalid Email or Password";
            return View();
        }
    }
}
