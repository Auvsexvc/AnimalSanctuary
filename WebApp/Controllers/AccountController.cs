using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using WebApp.Data;
using WebApp.Dtos;
using WebApp.Interfaces;
using WebApp.Models;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class AccountController : Controller
    {
        private readonly IAccountService _service;
        private readonly UserManagerService _userManagerService;

        public AccountController(IAccountService service, UserManagerService userManagerService)
        {
            _service = service;
            _userManagerService = userManagerService;
        }

        public async Task<IActionResult> Users()
        {
            var accessToken = _userManagerService.GetUserToken(HttpContext.Session.GetString("Id"));

            if (accessToken == null)
            {
                return View("AccessDenied");
            }

            var accounts = await _service.GetAllAccounts(accessToken);

            var loggedInUsers = _userManagerService.Users;
            var users = accounts.Where(x => !loggedInUsers.Select(x => x.Email).Contains(x.Email)).Select(x => new User() { Email = x.Email, Id = x.Id, Role = x.Role }).Concat(loggedInUsers).OrderByDescending(x => x.ValidTo);

            return View(users);
        }

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

            var user = await _service.GetAccount(loginVM);

            if (user == null)
            {
                //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                TempData["error"] = "Wrong credential. Please try again.";

                return View(loginVM);
            }

            var sessionId = Guid.NewGuid().ToString();
            HttpContext.Session.SetString("Id", sessionId);
            _userManagerService.AddUser(sessionId, user);

            ///

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));

            ///

            TempData["success"] = $"Successfully logged in as {user.Email}";

            return RedirectToAction("Index", "Animals");
        }

        public async Task<IActionResult> Register()
        {
            var accessToken = _userManagerService.GetUserToken(HttpContext.Session.GetString("Id"));

            if (accessToken == null)
            {
                return View("AccessDenied");
            }

            var dropdowns = await _service.GetNewUserDropdownsVM(accessToken);

            ViewBag.Roles = new SelectList(dropdowns.Roles, "Id", "Name");

            return View(new RegisterVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(RegisterVM registerVM)
        {
            var accessToken = _userManagerService.GetUserToken(HttpContext.Session.GetString("Id"));

            if (accessToken == null)
            {
                return View("AccessDenied");
            }

            var dropdowns = await _service.GetNewUserDropdownsVM(accessToken);

            ViewBag.Roles = new SelectList(dropdowns.Roles, "Id", "Name");

            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            var users = await _service.GetAllAccounts(accessToken);
            var user = users.FirstOrDefault(x => x.Email == registerVM.Email);

            if (user != null)
            {
                TempData["Error"] = $"{registerVM.Email} is already in use.";

                return View(registerVM);
            }

            using var newUserResult = await _service.RegisterAsync(registerVM, accessToken);

            if (!newUserResult.IsSuccessStatusCode)
            {
                TempData["Error"] = $"{registerVM.Email} couldn't be registered this time.";

                return View(registerVM);
            }

            return View("RegisterCompleted");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            var sessionId = HttpContext.Session.GetString("Id");

            var user = _userManagerService.GetUser(sessionId);

            if (string.IsNullOrEmpty(sessionId) || user == null)
            {
                return View();
            }

            _userManagerService.DeleteUser(sessionId);
            HttpContext.Session.Remove("Id");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            TempData["warning"] = $"{user.Email} logged out";

            return RedirectToAction("Index", "Animals");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}