using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogMVC.DTO;
using ProductCatalogMVC.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
            try
            {
                return View(new UserForLoginDto());
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"حدث خطأ أثناء تحميل صفحة تسجيل الدخول: {ex.Message}";
                return RedirectToAction("Index", "Products");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Login(UserForLoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return View(loginDto);

            var response = await _authService.LoginAsync(loginDto);
            if (!response.Success || response.Data == null)
            {
                TempData["Error"] = response.Message;
                return View(loginDto);
            }

            HttpContext.Session.SetString("JwtToken", response.Data.Token);

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(response.Data.Token);
            var claims = jwtToken.Claims.ToList();

            if (!claims.Any(c => c.Type == ClaimTypes.NameIdentifier))
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, jwtToken.Subject));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            TempData["Success"] = "تم تسجيل الدخول بنجاح";
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
