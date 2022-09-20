using Microsoft.AspNetCore.Mvc;
using WebApp.Dtos;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AnimalTypesController : Controller
    {
        private readonly IAnimalTypeService _service;

        public AnimalTypesController(IAnimalTypeService service)
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

        public IActionResult Create()
        {
            ViewBag.Session = HttpContext.Session.GetString("browser") ?? "true";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnimalTypeDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Type not added";
                return View();
            }

            await _service.CreateAsync(dto);
            TempData["success"] = "Type added";

            if (HttpContext.Session.GetString("browser") == "false")
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id) => await GetByIdAsync(id);

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

            TempData["success"] = "Type deleted";

            HttpContext.Session.SetString("return", String.Empty);

            if (HttpContext.Session.GetString("browser") == "false")
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id) => await GetByIdAsync(id);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, AnimalTypeViewModel data)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Type not updated";
                return View(data);
            }
            await _service.EditAsync(id, data);
            TempData["success"] = "Type updated";

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("return")))
            {
                return RedirectToAction("List");
            }

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

            filteringString = SessionHandlerForFiltering(filteringString);

            var sortingDropdown = _service.GetSortingDropdownsVM();
            (sortingField, sortingOrder) = SessionHandlerForSorting(sortingField, sortingOrder);
            sortingField = sortingDropdown.Fields.Contains(sortingField) ? sortingField : "Name";

            var data = await _service.GetAllAsync(sortingField, sortingOrder, filteringString);

            foreach (var prop in sortingDropdown.Fields)
            {
                ViewData[prop + "Order"] = sortingField == prop && sortingOrder != "desc" ? "desc" : "asc";
                ViewData[prop + "Field"] = prop;
            }

            ViewBag.Sorting = sortingDropdown;
            ViewBag.Field = sortingField;
            ViewBag.FieldDisplayName = sortingDropdown.DisplayNames.FirstOrDefault(x => x.Key == sortingField).Value;
            ViewBag.Order = sortingOrder;

            ViewBag.Filter = filteringString;

            return View(data);
        }

        private async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var data = await _service.GetByIdAsync(id);

            if (data == null)
            {
                return View("NotFound");
            }

            ViewBag.Session = HttpContext.Session.GetString("browser") ?? "true";
            ViewBag.SessionReturn = HttpContext.Session.GetString("return") ?? String.Empty;

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