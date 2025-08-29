using LoyaltyConsole.MVC.Areas.Admin.ViewModels.AuthVMs;
using LoyaltyConsole.MVC.Areas.Admin.ViewModels.CustomerVMs;
using LoyaltyConsole.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyConsole.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : BaseController
    {
        private readonly ICrudService _crudService;

        public UserController(ICrudService crudService)
        {
            _crudService = crudService;
        }

        public async Task<IActionResult> Index()
        {
            SetFullName();

            if (ViewBag.Role is null)
            {
                return RedirectToAction("AdminLogin", "Auth", new { area = "Admin" });
            }

            var datas = await _crudService.GetAllAsync<List<AuthGetVM>>("");
            return View(datas);
        }
    }
}
