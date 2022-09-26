using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Dtos;
using WebApp.Helpers;
using WebApp.Interfaces;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize(Roles = AccountRoles.Admin)]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly AccountManagerService _userManagerService;

        public AccountController(IAccountService accountService, AccountManagerService userManagerService)
        {
            _accountService = accountService;
            _userManagerService = userManagerService;
        }

        public async Task<IActionResult> Users()
        {
            var accessToken = _userManagerService.GetTokenBySessionId(HttpContext.Session.GetString("Id"));

            if (accessToken == null)
            {
                return View("AccessDenied");
            }

            var accounts = await _accountService.GetAllAccounts(accessToken);

            var users = _userManagerService.GetAccounts(accounts);

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

            var account = await _accountService.GetAccount(loginVM);

            if (account == null)
            {
                TempData["error"] = "Wrong credential. Please try again.";

                return View(loginVM);
            }

            var sessionId = Guid.NewGuid().ToString();
            HttpContext.Session.SetString("Id", sessionId);

            var user = _userManagerService.SignIn(sessionId, account);

            var identity = _userManagerService.GetIdentity(user);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, identity);

            TempData["success"] = $"Successfully logged in as {account.Email}";

            return RedirectToAction("Index", "Animals");
        }

        public async Task<IActionResult> Register()
        {
            var accessToken = _userManagerService.GetTokenBySessionId(HttpContext.Session.GetString("Id"));

            if (accessToken == null)
            {
                return View("AccessDenied");
            }

            var dropdowns = await _accountService.GetNewUserDropdownsVM(accessToken);

            ViewBag.Roles = new SelectList(dropdowns.Roles, "Id", "Name");

            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel registerVM)
        {
            var accessToken = _userManagerService.GetTokenBySessionId(HttpContext.Session.GetString("Id"));

            if (accessToken == null)
            {
                return View("AccessDenied");
            }

            var dropdowns = await _accountService.GetNewUserDropdownsVM(accessToken);

            ViewBag.Roles = new SelectList(dropdowns.Roles, "Id", "Name");

            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            var accounts = await _accountService.GetAllAccounts(accessToken);
            var account = accounts.FirstOrDefault(x => x.Email == registerVM.Email);

            if (account != null)
            {
                TempData["Error"] = $"{registerVM.Email} is already in use.";

                return View(registerVM);
            }

            using var newUserResult = await _accountService.RegisterAsync(registerVM, accessToken);

            if (newUserResult?.IsSuccessStatusCode != true)
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

            var user = _userManagerService.GetAccount(sessionId);

            if (string.IsNullOrEmpty(sessionId) || user == null)
            {
                return View();
            }

            _userManagerService.SignOut(sessionId);
            HttpContext.Session.Remove("Id");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            TempData["warning"] = $"{user.Email} logged out";

            return RedirectToAction("Index", "Animals");
        }
    }
}