using Microsoft.AspNetCore.Mvc;
using ProductCatalogMVC.DTO;
using ProductCatalogMVC.Services;

namespace ProductCatalogMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthService _authService;

        public AccountController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new UserForLoginDto());
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserForLoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return View(loginDto);

            var response = await _authService.LoginAsync(loginDto);
            if (!response.Success)
            {
                TempData["Error"] = response.Message;
                return View(loginDto);
            }

            return RedirectToAction("Index", "Products");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JwtToken");
            return RedirectToAction("Login");
        }
    }
}
