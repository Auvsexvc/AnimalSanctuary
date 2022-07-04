using Microsoft.AspNetCore.Mvc;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AnimalSanctuaryController : Controller
    {
        private readonly IAnimalSanctuaryService _animalSanctuaryService;

        public AnimalSanctuaryController(IAnimalSanctuaryService animalSanctuaryService)
        {
            _animalSanctuaryService = animalSanctuaryService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Animal obj)
        {
            if (!_animalSanctuaryService.IsEnoughSpace())
            {
                TempData["failure"] = "There is no room left to add animal";
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                _animalSanctuaryService.Create(obj);
                TempData["success"] = "Animal added";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var animalDb = _animalSanctuaryService.GetById(id);

            if (animalDb == null)
            {
                return NotFound();
            }
            return View(animalDb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var animalDb = _animalSanctuaryService.GetById(id);
            if (animalDb == null)
            {
                return NotFound();
            }

            _animalSanctuaryService.Delete(id);
            TempData["success"] = "Animal removed";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var animalDb = _animalSanctuaryService.GetById(id);

            if (animalDb == null)
            {
                return NotFound();
            }
            return View(animalDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Animal obj)
        {
            if (ModelState.IsValid)
            {
                _animalSanctuaryService.Update(obj);
                TempData["success"] = "Animal updated";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Index()
        {
            ViewBag.SpaceLeft = _animalSanctuaryService.GetSpaceLeft();
            return View(_animalSanctuaryService.GetAll());
        }
    }
}