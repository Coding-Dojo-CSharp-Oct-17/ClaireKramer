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
        private readonly NinjaFactory ninjaFactory;
        public DojoController() {
            ninjaFactory = new NinjaFactory();
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
                return RedirectToAction("Dojos");
            }
            else {
                ViewBag.errors = ModelState.Values;
                ViewBag.AllDojos = dojoFactory.FindAll();
                return View();
            }
        }

        [HttpGet]
        [Route("Dojos/{id}")]
        public IActionResult Dojo(int id) {
            ViewBag.Dojo = dojoFactory.GetById(id);
            var dojo = dojoFactory.GetNinjasById(id);
            ViewBag.Members = dojo.ninjas;
            ViewBag.Rogues = dojoFactory.GetRogues();
            return View();
        }

        [HttpGet]
        [Route("Banish/{id}")]
        public IActionResult Banish(int id) {
            ninjaFactory.Banish(id);
            return RedirectToAction("Dojos");
        }

        [HttpGet]
        [Route("Dojos/{dojo_id}/Recruit/{ninja_id}")]
        public IActionResult Recruit(int dojo_id, int ninja_id) {
            ninjaFactory.Recruit(dojo_id, ninja_id);
            return RedirectToAction("Ninjas", "Ninja");
        }
    }
}