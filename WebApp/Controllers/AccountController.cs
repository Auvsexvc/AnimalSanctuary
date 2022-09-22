﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Dtos;
using WebApp.Interfaces;
using WebApp.Models;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _service;
        private readonly UserManagerService _userManagerService;

        public AccountController(AccountService service, UserManagerService userManagerService)
        {
            _service = service;
            _userManagerService = userManagerService;
        }

        //[Authorize(Roles = UserRoles.Admin)]
        //public async Task<IActionResult> Users()
        //{
        //    var users = await _service.GetAllAsync<Account>();

        //    return View(users);
        //}

        [AllowAnonymous]
        public IActionResult Login() => View(new LoginDto());

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var result = await _service.GetToken(loginVM);

            if (result?.IsSuccessStatusCode != true)
            {
                TempData["error"] = "Wrong credential. Please try again.";

                return View(loginVM);
            }

            var user = await result.Content.ReadFromJsonAsync<Account>();

            //var token = await result.Content.ReadAsStringAsync();

            if(user != null)
            {
                var sessionId = Guid.NewGuid().ToString();
                HttpContext.Session.SetString("Id", sessionId);
                _userManagerService.AddUser(sessionId, user);

            }

            return RedirectToAction("Index", "Animals");

            //var result = await _baseService

            //var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            //if (user != null)
            //{
            //    var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);

            //    if (passwordCheck)
            //    {
            //        var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
            //        if (result.Succeeded)
            //        {
            //            return RedirectToAction("Index", "Movies");
            //        }
            //    }
            //    TempData["Error"] = "Wrong credential. Please try again.";

            //    return View(loginVM);
            //}

            //TempData["Error"] = "Wrong credential. Please try again.";

        }

        [AllowAnonymous]
        public IActionResult Register() => View(new RegisterVM());

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            //var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);

            //if (user != null)
            //{
            //    TempData["Error"] = $"{registerVM.EmailAddress} is already in use.";

            //    return View(registerVM);
            //}

            //var newUser = new AppUser()
            //{
            //    FullName = registerVM.FullName,
            //    Email = registerVM.EmailAddress,
            //    UserName = registerVM.EmailAddress
            //};

            //var newUserResult = await _userManager.CreateAsync(newUser, registerVM.Password);

            //if (newUserResult.Succeeded)
            //{
            //    await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            //}

            return View("RegisterCompleted");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            _userManagerService.DeleteUser(HttpContext.Session.GetString("Id"));
            HttpContext.Session.Remove("Id");

            return RedirectToAction("Index", "Animals");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
