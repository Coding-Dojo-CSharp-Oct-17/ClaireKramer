using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using bankaccounts.Models;
using System.Linq;

namespace bankaccounts.Controllers {
    public class AccountController : Controller {
        private Context _context;
        public AccountController(Context context) {
            _context = context;
        }

        [HttpGet]
        [Route("Account/{user_id}")]
        public IActionResult ViewAccount(int user_id) {
            if(HttpContext.Session.GetInt32("UserId") != user_id) {
                int? UserId = HttpContext.Session.GetInt32("UserId");
                return Redirect($"/Account/{UserId}");
            }
            User CurrentUser = _context.Users.SingleOrDefault(user => user.Id == HttpContext.Session.GetInt32("UserId"));
            List<Transcation> AllTranscations = _context.Transcations.OrderBy(Transcation => Transcation.CreatedAt).ToList();
            ViewBag.AllTranscations = AllTranscations;
            ViewBag.CurrentUser = CurrentUser;
            return View();
        }

        [HttpPost]
        [Route("Transcation")]
        public IActionResult Transcation(int Amount) {
            int? user_id = HttpContext.Session.GetInt32("UserId");
            User CurrentUser = _context.Users.SingleOrDefault(user => user.Id == HttpContext.Session.GetInt32("UserId"));
            if(CurrentUser.Balance - Amount < 0) {
                ViewBag.errors = "Insufficient Funds";
            }
            else {
                Transcation NewTranscation = new Transcation {
                    Amount = Amount,
                    CreatedAt = DateTime.Now,
                    User_Id = CurrentUser.Id,
                    User = CurrentUser
                };
                CurrentUser.Balance += Amount;
                _context.Add(NewTranscation);
                _context.SaveChanges();
            }
            return Redirect($"/account/{user_id}");
        }
    }
}