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
            return View(new UserForLoginDto());
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

            // خزن التوكن لاستخدامه في HttpClient لاحقًا
            HttpContext.Session.SetString("JwtToken", response.Data.Token);

            // استخراج claims من التوكن
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(response.Data.Token);
            var claims = jwtToken.Claims.ToList();

            // تأكد من وجود NameIdentifier وName وإلا أضفهم
            if (!claims.Any(c => c.Type == ClaimTypes.NameIdentifier))
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, jwtToken.Subject));
            }

            //if (!claims.Any(c => c.Type == ClaimTypes.Name))
            //{
            //    claims.Add(new Claim(ClaimTypes.Name, loginDto.Username)); // أو حسب اللي بيرجع من الـ API
            //}

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // تسجيل الدخول عبر الـ Cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            TempData["Success"] = "تم تسجيل الدخول بنجاح";
            return RedirectToAction("Index", "Products");
        }


        //[HttpPost]
        //public async Task<IActionResult> Login(UserForLoginDto loginDto)
        //{
        //    if (!ModelState.IsValid)
        //        return View(loginDto);

        //    var response = await _authService.LoginAsync(loginDto);
        //    if (!response.Success)
        //    {
        //        TempData["Error"] = response.Message;
        //        return View(loginDto);
        //    }

        //    return RedirectToAction("Index", "Products");
        //}


        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JwtToken");
            return RedirectToAction("Login");
        }
    }
}
