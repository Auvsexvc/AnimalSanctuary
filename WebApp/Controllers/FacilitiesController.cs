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
    public class FacilitiesController : Controller
    {
        private readonly IFacilityService _service;
        private readonly UserManagerService _userManagerService;

        public FacilitiesController(IFacilityService service, UserManagerService userManagerService)
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

            var dropdowns = await _service.GetNewFacilityDropdownsVM();

            ViewBag.Animals = new SelectList(dropdowns.Animals, "Id", "Name");

            ViewBag.Session = HttpContext.Session.GetString("browser") ?? "true";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FacilityDto dto)
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
                TempData["success"] = "Facility added";
            }
            else
            {
                TempData["error"] = "Facility not added";

                return await Create();
            }

            if (HttpContext.Session.GetString("browser") == "false")
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("CreateMod")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMod(FacilityDto dto)
        {
            var accessToken = _userManagerService.GetUserToken(HttpContext.Session.GetString("Id"));

            if (accessToken == null)
            {
                return View("AccessDenied");
            }

            if (!ModelState.IsValid)
            {
                TempData["warning"] = "Check fields";
            }

            var result = await _service.CreateAsync(dto, accessToken);

            if (result?.IsSuccessStatusCode == true)
            {
                TempData["success"] = "Facility added";
            }
            else
            {
                TempData["error"] = "Facility not added";
            }

            return Redirect(Request.Headers["Referer"]);
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
                TempData["success"] = "Facility deleted";
            }
            else
            {
                TempData["error"] = "Facility not deleted";

                return await Delete(id);
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

            var dropdowns = await _service.GetNewFacilityDropdownsVM();
            var data = await _service.GetByIdUpdateModelAsync(id);

            if (data == null)
            {
                return View("NotFound");
            }

            ViewBag.DropDowns = dropdowns;
            ViewBag.AnimalString = data.Animals != null ? string.Join(", ", data.Animals.Select(x => x.Name)) : string.Empty;
            ViewBag.Animals = new SelectList(dropdowns.Animals, "Id", "Name");

            ViewBag.Session = HttpContext.Session.GetString("browser") ?? "true";
            ViewBag.SessionReturn = HttpContext.Session.GetString("return") ?? String.Empty;

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateFacilityViewModel data)
        {
            var accessToken = _userManagerService.GetUserToken(HttpContext.Session.GetString("Id"));

            if (accessToken == null)
            {
                return View("AccessDenied");
            }

            if (!ModelState.IsValid)
            {
                TempData["warning"] = "Check fields";

                return View(data);
            }
            var result = await _service.EditAsync(id, data, accessToken);

            if (result?.IsSuccessStatusCode == true)
            {
                TempData["success"] = "Facility updated";
            }
            else
            {
                TempData["error"] = "Facility not updated";

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