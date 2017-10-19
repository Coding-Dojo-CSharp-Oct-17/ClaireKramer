using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DbConnection;
using wall.Models;

namespace wall.Controllers {
    public class WallController : Controller {
        [HttpGet]
        [Route("Home")]

        //Note(Devon):
        // It is more common to make Index method here, and use Views folder structure to determine which controller maps to which view file, rather than serve only Shared Views.
        public IActionResult Home() {
            if(HttpContext.Session.GetInt32("userId") == null) {
                return RedirectToAction("Index", "User");
            }
            string query = "SELECT messages.id, messages.message, messages.created_at, users.first_name, users.last_name FROM messages JOIN users ON messages.user_id = users.id;";
            string query2 = "SELECT comments.id, comments.comment, comments.created_at, users.first_name, users.last_name, comments.message_id, comments.user_id FROM comments JOIN messages ON comments.message_id = messages.id JOIN users ON comments.user_id = users.id;";
            var AllMessages = DbConnector.Query(query);
            var AllComments = DbConnector.Query(query2);
            ViewBag.AllMessages = AllMessages;
            ViewBag.AllComments = AllComments;
            ViewBag.Name = HttpContext.Session.GetString("name");
            ViewBag.UserId = HttpContext.Session.GetInt32("userId");
            return View();
        }

        [HttpPost]
        [Route("PostMessage")]
        public IActionResult PostMessage(Message model) {
            if(ModelState.IsValid) {
                string query = $"INSERT INTO messages (user_id, message, created_at, updated_at) VALUES ('{(int)HttpContext.Session.GetInt32("userId")}', '{model.Content}', NOW(), NOW());";
                DbConnector.Execute(query);
                return RedirectToAction("Home");
            }
            else {
                ViewBag.errors = ModelState.Values;
                return View();
            }
        }

        [HttpPost]
        [Route("PostComment")]
        public IActionResult PostComment(Comment model) {
            if(ModelState.IsValid) {
                string query = $"INSERT INTO comments (message_id, user_id, comment, created_at, updated_at) VALUES ('{model.MessageId}', '{(int)HttpContext.Session.GetInt32("userId")}', '{model.Content}', NOW(), NOW());";
                DbConnector.Execute(query);
                return RedirectToAction("Home");
            }
            else {
                ViewBag.error = ModelState.Values;
                return View();
            }
        }

        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout() {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "User");
        }
    }
}