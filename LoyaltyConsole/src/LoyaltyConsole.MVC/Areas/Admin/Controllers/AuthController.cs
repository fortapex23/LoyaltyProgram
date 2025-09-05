using LoyaltyConsole.MVC.Areas.Admin.ViewModels.AuthVMs;
using LoyaltyConsole.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyConsole.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            return View("Login");
        }

        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminLogin(UserLoginVM vm)
        {
            if (!ModelState.IsValid) return View();

            LoginResponseVM data = null;

            try
            {
                data = await _authService.AdminLogin(vm);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Email or password is incorrect");
                return View();
            }

            if (data == null)
            {
                ModelState.AddModelError("", "Something went wrong");
                return View();
            }

            HttpContext.Response.Cookies.Append("token", data.AccessToken, new CookieOptions
            {
                Expires = data.ExpireDate,
                HttpOnly = true
            });

            return RedirectToAction("index", "home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterVM vm)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                var data = await _authService.Register(vm);

                if (data)
                {
                    return RedirectToAction("AdminLogin");
                }
                else
                {
                    ModelState.AddModelError("", "can't register");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        public IActionResult Logout()
        {
            _authService.Logout();

            return RedirectToAction("adminlogin");
        }
    }
}
