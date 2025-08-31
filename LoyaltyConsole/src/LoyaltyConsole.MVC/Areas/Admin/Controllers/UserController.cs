using LoyaltyConsole.Business.DTOs.UserDtos;
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

            if (ViewBag.Role is null) return RedirectToAction("AdminLogin", "Auth", new { area = "Admin" });

            if (ViewBag.Role != "SuperAdmin") return RedirectToAction("home", "Index", new { area = "Admin" });

            var datas = await _crudService.GetAllAsync<List<AuthGetVM>>("/auth/GetAllAdmins");
            return View(datas);
        }

        public async Task<IActionResult> ApproveAdmin(string id)
        {
            SetFullName();
            if (ViewBag.Role is null) return RedirectToAction("AdminLogin", "Auth", new { area = "Admin" });

            if (ViewBag.Role != "SuperAdmin") return RedirectToAction("home", "Index", new { area = "Admin" });

            var user = await _crudService.GetByStringIdAsync<AuthGetVM>($"/auth/{id}", id);
            if (user == null) return NotFound();

            var vm = new AuthEditVM
            {
                Status = AdminStatus.Approved,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                BirthDay = user.BirthDay,
                Gender = user.Gender
            };

            try
            {
                await _crudService.Update($"/auth/{id}", vm);
            }
            catch
            {
                ModelState.AddModelError("", "Error approving admin");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RejectAdmin(string id)
        {
            SetFullName();
            if (ViewBag.Role is null) return RedirectToAction("AdminLogin", "Auth", new { area = "Admin" });

            if (ViewBag.Role != "SuperAdmin") return RedirectToAction("home", "Index", new { area = "Admin" });

            var user = await _crudService.GetByStringIdAsync<AuthGetVM>($"/auth/{id}", id);
            if (user == null) return NotFound();

            var vm = new AuthEditVM
            {
                Status = AdminStatus.Rejected,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                BirthDay = user.BirthDay,
                Gender = user.Gender
            };

            try
            {
                await _crudService.Update($"/auth/{id}", vm);
            }
            catch
            {
                ModelState.AddModelError("", "Error rejecting admin");
            }
            return RedirectToAction("Index");
        }

    }
}
