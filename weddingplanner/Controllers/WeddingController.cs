using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using weddingplanner.Models;
using System.Linq;

namespace weddingplanner.Controllers {
    public class WeddingController : Controller {
        private WeddingContext _context;
        public WeddingController(WeddingContext context) {
            _context = context;
        }
        [HttpGet]
        [Route("Dashboard")]
        public IActionResult Dashboard()
        {
            if(HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Index", "User");
            }
            List<Wedding> AllWeddings = _context.Weddings
                                        .Include(wedding => wedding.Guests)
                                        .ToList();
            User CurrentUser = _context.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.AllWeddings = AllWeddings;
            ViewBag.CurrentUser = CurrentUser;
            return View();
        }

        [HttpGet]
        [Route("NewWedding")]
        public IActionResult NewWedding() {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            return View();
        }

        [HttpPost]
        [Route("AddWedding")]
        public IActionResult AddWedding(Wedding model) {
            if(model.WeddingDate < DateTime.Now) {
                ModelState.AddModelError("WeddingDate", "Wedding must be in the future");
            }
            if(ModelState.IsValid) {
                _context.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            else {
                ViewBag.errors = ModelState.Values;
                return View("NewWedding");
            }
        }

        [HttpGet]
        [Route("Wedding/{WeddingId}")]
        public IActionResult Wedding(int WeddingId) {
            Wedding CurrentWedding = _context.Weddings
                                        .Include(wedding => wedding.Guests)
                                        .SingleOrDefault(wedding => wedding.WeddingId == WeddingId);
            ViewBag.CurrentWedding = CurrentWedding;
            return View("Wedding");
        }

        [HttpGet]
        [Route("RSVP/{WeddingId}")]
        public IActionResult RSVP(int WeddingId) {
            User CurrentUser = _context.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            Wedding CurrentWedding = _context.Weddings
                                            .Include(wedding => wedding.Guests)
                                            .SingleOrDefault(wedding => wedding.WeddingId == WeddingId);
            CurrentWedding.Guests.Add(CurrentUser);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("Decline/{WeddingId}")]
        public IActionResult Decline(int WeddingId) {
            User CurrentUser = _context.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            Wedding CurrentWedding = _context.Weddings
                                            .Include(wedding => wedding.Guests)
                                            .SingleOrDefault(wedding => wedding.WeddingId == WeddingId);
            CurrentWedding.Guests.Remove(CurrentUser);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("Delete/{WeddingId}")]
        public IActionResult Delete(int WeddingId) {
            Wedding CurrentWedding = _context.Weddings
                                            .SingleOrDefault(wedding => wedding.WeddingId == WeddingId);
            _context.Weddings.Remove(CurrentWedding);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
    }
}
