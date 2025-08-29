using LoyaltyConsole.MVC.Areas.Admin.ViewModels.AdminVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyConsole.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : BaseController
    {
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Index()
        {
            SetFullName();

            if (ViewBag.Role is null)
            {
                return RedirectToAction("AdminLogin", "Auth", new { area = "Admin" });
            }

            var adminvm = new AdminVM()
            {
                FullName = ViewBag.FullName,
            };

            return View(adminvm);
        }
    }
}
