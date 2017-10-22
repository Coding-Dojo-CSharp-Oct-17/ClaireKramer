using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using dojoleague.Models;
using dojoleague.Factory;

namespace dojoleague.Controllers
{
    public class NinjaController : Controller
    {
        private readonly NinjaFactory ninjaFactory;
        private readonly DojoFactory dojoFactory;
        public NinjaController() {
            ninjaFactory = new NinjaFactory();
            dojoFactory = new DojoFactory();
        }

        [HttpGet]
        [Route("Ninjas")]
        public IActionResult Ninjas()
        {
            ViewBag.AllNinjas = ninjaFactory.FindAll();
            ViewBag.AllDojos = ninjaFactory.AllDojos();
            return View("Ninjas");
        }

        [HttpPost]
        [Route("AddNinja")]
        public IActionResult AddNinja(Ninja model, int dojo_id) {
            if(ModelState.IsValid) {
                Dojo dojo = dojoFactory.GetById(dojo_id);
                model.dojo = dojo;
                ninjaFactory.AddNinja(model);
                return RedirectToAction("Ninjas", "Ninja");
            }
            else{
                ViewBag.errors = ModelState.Values;
                ViewBag.AllNinjas = ninjaFactory.FindAll();
                ViewBag.AllDojos = ninjaFactory.AllDojos();
                return View("Ninjas");
            }
        }

        [HttpGet]
        [Route("Ninjas/{id}")]
        public IActionResult Ninja(int id) {
            Ninja ninja = ninjaFactory.FindByID(id);
            ViewBag.Ninja = ninja;
            return View("Ninja", "Ninja");
        }
    }
}
