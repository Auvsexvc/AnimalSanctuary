using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Dtos;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly IAnimalService _service;

        public AnimalsController(IAnimalService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string? sortingField, string? sortingOrder, string? filteringString = "")
        {
            HttpContext.Session.SetString("browser", "true");

            return await GetAllSortedAndFiltered(sortingField, sortingOrder, filteringString);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            HttpContext.Session.SetString("return", "Details");

            return await GetByIdAsync(id);
        }

        public async Task<IActionResult> Create()
        {
            var dropdowns = await _service.GetNewAnimalDropdownsVM();

            ViewBag.Session = HttpContext.Session.GetString("browser") ?? "true";
            ViewBag.Species = new SelectList(dropdowns.Species, "Id", "Name");
            ViewBag.Facilities = new SelectList(dropdowns.Facilities, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnimalDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Animal not added";
                return View();
            }

            await _service.CreateAsync(dto);
            TempData["success"] = "Animal added";

            if (HttpContext.Session.GetString("browser") == "false")
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            return await GetByIdAsync(id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(Guid id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            await _service.DeleteAsync(id);

            TempData["success"] = "Animal deleted";

            HttpContext.Session.SetString("return", String.Empty);
            if (HttpContext.Session.GetString("browser") == "false")
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            return await GetByIdAsync(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, AnimalViewModel data)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Animal not updated";
                return View(data);
            }
            await _service.EditAsync(id, data);
            TempData["success"] = "Animal updated";

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("return")))
            {
                return RedirectToAction("List");
            }

            HttpContext.Session.SetString("return", String.Empty);
            return RedirectToAction("Details", new { id });
        }

        public async Task<IActionResult> List(string? sortingField, string? sortingOrder, string? filteringString = "")
        {
            HttpContext.Session.SetString("browser", "false");

            return await GetAllSortedAndFiltered(sortingField, sortingOrder, filteringString);
        }

        private async Task<IActionResult> GetAllSortedAndFiltered(string? sortingField, string? sortingOrder, string? filteringString = "")
        {
            HttpContext.Session.SetString("return", String.Empty);
            (sortingField, sortingOrder) = SessionHandlerForSorting(sortingField, sortingOrder);
            filteringString = SessionHandlerForFiltering(filteringString);

            var data = await _service.GetAllAsync(sortingField, sortingOrder, filteringString);
            var sortingDropdown = _service.GetAnimalSortingDropdownsVM();

            foreach (var prop in sortingDropdown.Fields)
            {
                ViewData[prop + "Order"] = sortingField == prop && sortingOrder != "desc" ? "desc" : "asc";
                ViewData[prop + "Field"] = prop;
            }

            ViewBag.Field = sortingField;
            ViewBag.Fields = sortingDropdown.Fields;
            ViewBag.OrderList = sortingDropdown.Order;
            ViewBag.Order = sortingOrder;
            ViewBag.Filter = filteringString;

            return View(data);
        }

        private async Task<IActionResult> GetByIdAsync(Guid id)
        {
            ViewBag.Session = HttpContext.Session.GetString("browser") ?? "true";
            ViewBag.SessionReturn = HttpContext.Session.GetString("return") ?? String.Empty;

            var dropdowns = await _service.GetNewAnimalDropdownsVM();

            var data = await _service.GetByIdAsync(id);

            if (data == null)
            {
                return View("NotFound");
            }

            ViewBag.DropDowns = dropdowns;
            ViewBag.Specie = data.Specie;
            ViewBag.Species = new SelectList(dropdowns.Species, "Id", "Name");
            ViewBag.Facilities = new SelectList(dropdowns.Facilities, "Id", "Name");
            ViewBag.Types = new SelectList(dropdowns.Types, "Id", "Name");

            return View(data);
        }

        private string? SessionHandlerForFiltering(string? filterString)
        {
            if (filterString == null)
            {
                HttpContext.Session.SetString("searchString", "");
                filterString = "";
            }
            else if (filterString != "")
            {
                HttpContext.Session.SetString("searchString", filterString);
            }
            else
            {
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("searchString")))
                {
                    filterString = HttpContext.Session.GetString("searchString");
                }
                else
                {
                    HttpContext.Session.SetString("searchString", String.Empty);
                    filterString = String.Empty;
                }
            }

            return filterString;
        }

        private (string sortingField, string sortingOrder) SessionHandlerForSorting(string? sortingField, string? sortingOrder)
        {
            if (sortingOrder != null)
            {
                HttpContext.Session.SetString("sortingOrder", sortingOrder);
            }
            else
            {
                sortingOrder = HttpContext.Session.GetString("sortingOrder") ?? "asc";
            }

            if (sortingField != null)
            {
                HttpContext.Session.SetString("sortingField", sortingField);
            }
            else
            {
                sortingField = HttpContext.Session.GetString("sortingField") ?? "Name";
            }

            return (sortingField, sortingOrder);
        }
    }
}