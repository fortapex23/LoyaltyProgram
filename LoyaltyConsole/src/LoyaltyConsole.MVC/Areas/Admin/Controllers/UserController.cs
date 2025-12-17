using LoyaltyConsole.MVC.Areas.Admin.ViewModels.AuthVMs;
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
                return RedirectToAction("AdminLogin", "Auth", new { area = "Admin" });

            if (ViewBag.Role != "SuperAdmin")
                return RedirectToAction("Index", "Home", new { area = "Admin" });

            var users =
                await _crudService.GetAsync<List<AuthGetVM>>("/auth/GetAllAdmins");

            return View(users);
        }

        // ---------------- APPROVE ----------------

        public async Task<IActionResult> ApproveAdmin(string id)
        {
            return await ChangeStatus(id, AdminStatus.Approved);
        }

        // ---------------- REJECT ----------------

        public async Task<IActionResult> RejectAdmin(string id)
        {
            return await ChangeStatus(id, AdminStatus.Rejected);
        }

        // ---------------- SHARED LOGIC ----------------

        private async Task<IActionResult> ChangeStatus(string id, AdminStatus status)
        {
            SetFullName();

            if (ViewBag.Role is null)
                return RedirectToAction("AdminLogin", "Auth", new { area = "Admin" });

            if (ViewBag.Role != "SuperAdmin")
                return RedirectToAction("Index", "Home", new { area = "Admin" });

            try
            {
                var user =
                    await _crudService.GetAsync<AuthGetVM>($"/auth/{id}");

                if (user == null)
                    return NotFound();

                var vm = new AuthEditVM
                {
                    Status = status,
                    FullName = user.FullName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    BirthDay = user.BirthDay,
                    Gender = user.Gender
                };

                await _crudService.UpdateAsync($"/auth/{id}", vm);
            }
            catch
            {
                TempData["Error"] = "Operation failed";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
