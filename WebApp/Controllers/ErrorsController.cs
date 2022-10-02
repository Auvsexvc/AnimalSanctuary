using Microsoft.AspNetCore.Mvc;

namespace WebClientApp.Controllers
{
    public class ErrorsController : Controller
    {
        public IActionResult Error404()
        {
            return View("NotFound");
        }

        public IActionResult Error401()
        {
            return View("AccessDenied");
        }
    }
}