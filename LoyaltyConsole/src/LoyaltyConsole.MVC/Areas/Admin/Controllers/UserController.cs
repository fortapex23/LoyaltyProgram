using LoyaltyConsole.MVC.Areas.Admin.ViewModels.AuthVMs;
using LoyaltyConsole.MVC.Areas.Admin.ViewModels.CustomerVMs;
using LoyaltyConsole.MVC.Enums;
using LoyaltyConsole.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyConsole.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : BaseController
    {
        private readonly ICrudService _crudService;
        private readonly IAuthService _authService;

        public UserController(ICrudService crudService, IAuthService authService)
        {
            _crudService = crudService;
            _authService = authService;
        }

        public async Task<IActionResult> Index()
        {
            SetFullName();

            if (ViewBag.Role is null)
            {
                return RedirectToAction("AdminLogin", "Auth", new { area = "Admin" });
            }

            var datas = await _crudService.GetAllAsync<List<AuthGetVM>>("/auth/GetAllAdmins");
            return View(datas);
        }

        public async Task<IActionResult> ApproveAdmin(string id)
        {
            try
            {
                await _authService.Update($"/auth/{id}/status", new AuthEditVM(AdminStatus.Approved));
            }
            catch
            {
                ModelState.AddModelError("", "Error approving admin");
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RejectAdmin(string id)
        {
            try
            {
                await _authService.Update($"/auth/{id}/status", new AuthEditVM(AdminStatus.Rejected));
            }
            catch
            {
                ModelState.AddModelError("", "Error rejecting admin");
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


    }
}
