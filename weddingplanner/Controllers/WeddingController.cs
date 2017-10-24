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
            List<Wedding> Weddings = _context.Weddings
                                            .Include(wedding => wedding.Guests)
                                            .ToList();
            ViewBag.AllWeddings = Weddings;
            return View();
        }

        [HttpGet]
        [Route("NewWedding")]
        public IActionResult NewWedding() {
            return View();
        }

        [HttpPost]
        [Route("AddWedding")]
        public IActionResult AddWedding(Wedding model) {
            if(ModelState.IsValid) {
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
    }
}
