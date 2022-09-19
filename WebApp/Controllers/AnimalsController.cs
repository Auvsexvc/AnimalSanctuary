﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
using WebApp.Data;
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
            (sortingField, sortingOrder) = SessionHandlerForSorting(sortingField, sortingOrder);
            filteringString = SessionHandlerForSearching(filteringString);

            var data = await _service.GetAllAsync(sortingField, sortingOrder, filteringString);

            foreach (var prop in new FilterAnimalViewModel().GetType().GetProperties().Select(p => p.Name))
            {
                ViewData[prop + "Order"] = sortingField == prop && sortingOrder != "desc" ? "desc" : "asc";
                ViewData[prop + "Field"] = prop;
            }

            ViewBag.Field = sortingField;
            ViewBag.Fields = new FilterAnimalViewModel().GetType().GetProperties().Select(p => p.Name);
            ViewBag.OrderList = new string[] { "asc", "desc" };
            //ViewBag.Fields = new SelectList(new FilterAnimalViewModel().GetType().GetProperties().Select(p => p.Name), sortingField);
            //ViewBag.OrderList = new SelectList(new string[] { "asc", "desc" }, sortingOrder);
            ViewBag.Order = sortingOrder;
            ViewBag.Filter = filteringString;

            return View(data);
        }

        public async Task<IActionResult> Details(Guid id) => await GetByIdAsync(id);

        public async Task<IActionResult> Create()
        {
            var dropdowns = await _service.GetNewAnimalDropdownsVM();

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
                return View();
            }

            await _service.CreateAsync(dto);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id) => await GetByIdAsync(id);

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(Guid id)
        {
            var animalDb = await _service.GetByIdAsync(id);
            if (animalDb == null)
            {
                return NotFound();
            }

            await _service.DeleteAsync(id);
            TempData["success"] = "Animal removed";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id) => await GetByIdAsync(id);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, AnimalViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            await _service.EditAsync(id, data);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> List(string? sortingField, string? sortingOrder, string? filteringString = "")
        {
            ViewData["Reffer"] = Request.Headers["Referer"].ToString();

            return await Index(sortingField, sortingOrder, filteringString);
        }

        private async Task<IActionResult> GetByIdAsync(Guid id)
        {
            if (Request.Headers["Referer"] != string.Empty && !Request.Headers["Referer"].ToString().Contains("Index"))
            {
                ViewData["Reffer"] = Request.Headers["Referer"].ToString();
            }

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

        private string? SessionHandlerForSearching(string? filterString)
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