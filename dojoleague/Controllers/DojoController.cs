using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using dojoleague.Models;
using dojoleague.Factory;

namespace dojoleague.Controllers
{
    public class DojoController : Controller
    {
        private readonly DojoFactory dojoFactory;
        public DojoController() {
            dojoFactory = new DojoFactory();
        }

        [HttpGet]
        [Route("Dojos")]
        public IActionResult Dojos()
        {
            ViewBag.AllDojos = dojoFactory.FindAll();
            return View();
        }

        [HttpPost]
        [Route("AddDojo")]
        public IActionResult AddDojo(Dojo model) {
            if(ModelState.IsValid) {
                dojoFactory.AddDojo(model);
                return RedirectToAction("Dojos", "Dojo");
            }
            else {
                ViewBag.errors = ModelState.Values;
                ViewBag.AllDojos = dojoFactory.FindAll();
                return View();
            }
        }
    }
}