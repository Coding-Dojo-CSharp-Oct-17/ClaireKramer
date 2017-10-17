using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DbConnection;
using wall.Models;
using wall.Controllers;

namespace wall.Controllers {
    public class WallController : Controller {
        [HttpGet]
        [Route("Home")]

        //Note(Devon):
        // It is more common to make Index method here, and use Views folder structure to determine which controller maps to which view file, rather than serve only Shared Views.
        public IActionResult Home() {
            if(HttpContext.Session.GetInt32("userId") == null) {
                return RedirectToAction("Index");
            }
            string query = "SELECT * FROM messages ORDER BY created_at DESC;";
            var AllMessages = DbConnector.Query(query);
            ViewBag.AllMessages = AllMessages;
            ViewBag.Name = HttpContext.Session.GetString("name");
            ViewBag.UserId = HttpContext.Session.GetInt32("id");
            return View();
        }

        [HttpPost]
        [Route("PostMessage")]
        public IActionResult PostMessage(Message model) {
            if(ModelState.IsValid) {
                string query = $"INSERT INTO messages (user_id, message, created_at, updated_at) VALUES ('{HttpContext.Session.GetInt32("userId")}', '{model.Content}', NOW(), NOW();";
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
        public IActionResult PostComment(Comment comment) {
            if(ModelState.IsValid) {
                string query = $"INSERT INTO comments (message_id, user_id, comment, created_at, updated_at) VALUES ('{comment.MessageId}', '{comment.UserId}', '{comment.Content}', NOW(), NOW();";
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
            return RedirectToAction("Index");
        }
    }
}