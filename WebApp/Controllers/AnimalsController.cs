using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Data;
using WebApp.Dtos;
using WebApp.Interfaces;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class AnimalsController : Controller
    {
        private readonly IAnimalService _service;
        private readonly UserManagerService _userManagerService;

        public AnimalsController(IAnimalService service, UserManagerService userManagerService)
        {
            _service = service;
            _userManagerService = userManagerService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string? sortingField, string? sortingOrder, string? filteringString = "")
        {
            HttpContext.Session.SetString("browser", "true");

            return await GetAllSortedAndFiltered(sortingField, sortingOrder, filteringString);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            HttpContext.Session.SetString("return", "Details");

            return await GetByIdAsync(id);
        }

        public async Task<IActionResult> Create()
        {
            var accessToken = _userManagerService.GetUserToken(HttpContext.Session.GetString("Id"));

            if (accessToken == null)
            {
                return View("AccessDenied");
            }

            var dropdowns = await _service.GetNewUserDropdownsVM();

            ViewBag.Species = new SelectList(dropdowns.Species, "Id", "Name");
            ViewBag.Facilities = new SelectList(dropdowns.Facilities, "Id", "Name");
            ViewBag.Types = new SelectList(dropdowns.Types, "Id", "Name");

            ViewBag.Session = HttpContext.Session.GetString("browser") ?? "true";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnimalDto dto)
        {
            var accessToken = _userManagerService.GetUserToken(HttpContext.Session.GetString("Id"));

            if (accessToken == null)
            {
                return View("AccessDenied");
            }

            if (!ModelState.IsValid)
            {
                TempData["warning"] = "Check fields";

                return await Create();
            }

            var result = await _service.CreateAsync(dto, accessToken);

            if (result?.IsSuccessStatusCode == true)
            {
                TempData["success"] = "Animal added";
            }
            else
            {
                TempData["error"] = "Animal not added";

                return await Create();
            }

            if (HttpContext.Session.GetString("browser") == "false")
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var accessToken = _userManagerService.GetUserToken(HttpContext.Session.GetString("Id"));

            if (accessToken == null)
            {
                return View("AccessDenied");
            }

            return await GetByIdAsync(id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(Guid id)
        {
            var accessToken = _userManagerService.GetUserToken(HttpContext.Session.GetString("Id"));

            if (accessToken == null)
            {
                return View("AccessDenied");
            }

            var data = await _service.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            var result = await _service.DeleteAsync(id, accessToken);

            if (result?.IsSuccessStatusCode == true)
            {
                TempData["success"] = "Animal deleted";
            }
            else
            {
                TempData["error"] = "Animal not deleted";

                return await Edit(id);
            }

            HttpContext.Session.SetString("return", String.Empty);

            if (HttpContext.Session.GetString("browser") == "false")
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var accessToken = _userManagerService.GetUserToken(HttpContext.Session.GetString("Id"));

            if (accessToken == null)
            {
                return View("AccessDenied");
            }

            var dropdowns = await _service.GetNewUserDropdownsVM();
            var data = await _service.GetByIdUpdateModelAsync(id);

            if (data == null)
            {
                return View("NotFound");
            }

            ViewBag.DropDowns = dropdowns;
            ViewBag.Specie = data.Specie;
            ViewBag.Species = new SelectList(dropdowns.Species, "Id", "Name");
            ViewBag.Facilities = new SelectList(dropdowns.Facilities, "Id", "Name");
            ViewBag.Types = new SelectList(dropdowns.Types, "Id", "Name");

            ViewBag.Session = HttpContext.Session.GetString("browser") ?? "true";
            ViewBag.SessionReturn = HttpContext.Session.GetString("return") ?? String.Empty;

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateAnimalViewModel data)
        {
            var accessToken = _userManagerService.GetUserToken(HttpContext.Session.GetString("Id"));

            if (accessToken == null)
            {
                return View("AccessDenied");
            }

            if (!ModelState.IsValid)
            {
                TempData["warning"] = "Check fields";

                return await Edit(id);
            }
            var result = await _service.EditAsync(id, data, accessToken);

            if (result?.IsSuccessStatusCode == true)
            {
                TempData["success"] = "Animal updated";
            }
            else
            {
                TempData["error"] = "Animal not updated";

                return await Edit(id);
            }

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("return")))
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Details", new { id });
        }

        public async Task<IActionResult> List(string? sortingField, string? sortingOrder, string? filteringString = "")
        {
            HttpContext.Session.SetString("browser", "false");

            var dropdowns = await _service.GetNewUserDropdownsVM();
            ViewBag.DropDowns = dropdowns;
            ViewBag.Species = new SelectList(dropdowns.Species, "Id", "Name");
            ViewBag.Facilities = new SelectList(dropdowns.Facilities, "Id", "Name");
            ViewBag.Types = new SelectList(dropdowns.Types, "Id", "Name");

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

            //if (data != null)
            //{
            //    foreach (var animalViewModel in data)
            //    {
            //        ViewData[$"{animalViewModel.Id}"] = await _service.GetByIdUpdateModelAsync(animalViewModel.Id);
            //    }
            //}

            ViewBag.Sorting = sortingDropdown;
            ViewBag.Field = sortingField;
            ViewBag.FieldDisplayName = sortingDropdown.DisplayNames.FirstOrDefault(x => x.Key == sortingField).Value;
            ViewBag.Order = sortingOrder;
            ViewBag.All = data;

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
                sortingOrder = HttpContext.Session.GetString("sortingOrder") ?? "desc";
            }

            if (sortingField != null)
            {
                HttpContext.Session.SetString("sortingField", sortingField);
            }
            else
            {
                sortingField = HttpContext.Session.GetString("sortingField") ?? "DateCreated";
            }

            return (sortingField, sortingOrder);
        }
    }
}