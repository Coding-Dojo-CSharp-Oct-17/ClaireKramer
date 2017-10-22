using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using restauranter.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace restauranter.Controllers
{
    public class ReviewController : Controller
    {
        private Context _context;
        public ReviewController(Context context) {
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("AddReview")]
        public IActionResult AddReview(Review model) {
            if(model.VisitDate > DateTime.Now) {
                ModelState.AddModelError("VisitDate", "Date of Visit cannot be in the future");
            }
            if(ModelState.IsValid) {
                _context.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Reviews");
            }
            else {
                ViewBag.errors = ModelState.Values;
                return View("Index");
            }
        }

        [HttpGet]
        [Route("Reviews")]
        public IActionResult Reviews() {
            List<Review> AllReviews = _context.Review.OrderBy(Review => Review.VisitDate).ToList();
            ViewBag.AllReviews = AllReviews;
            return View();
        }
    }
}
