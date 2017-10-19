using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using trails.Models;
using trails.Factory;

namespace trails.Controllers
{
    public class TrailController : Controller
    {
        private readonly TrailFactory trailFactory;
        public TrailController() {
            trailFactory = new TrailFactory();
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.AllTrails = trailFactory.FindAll();
            return View();
        }

        [HttpGet]
        [Route("NewTrail")]
        public IActionResult NewTrail() {
            return View();
        }

        [HttpPost]
        [Route("AddTrail")]
        public IActionResult AddTrail(Trail model) {
            if(ModelState.IsValid) {
                trailFactory.AddTrail(model);
                return RedirectToAction("Index");
            }
            else {
                ViewBag.errors = ModelState.Values;
                return View();
            }
        }

        [HttpGet]
        [Route("Trail/{id}")]
        public IActionResult Trail(int id) {
             ViewBag.Trail = trailFactory.FindByID(id);
            return View();
        }
    }
}
