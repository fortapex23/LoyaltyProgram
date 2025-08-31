using LoyaltyConsole.MVC.Areas.Admin.ViewModels.CashbackBalanceVMs;
using LoyaltyConsole.MVC.Areas.Admin.ViewModels.CustomerVMs;
using LoyaltyConsole.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyConsole.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CashbackBalanceController : BaseController
    {
        private readonly ICrudService _crudService;

        public CashbackBalanceController(ICrudService crudService)
        {
            _crudService = crudService;
        }

        public async Task<IActionResult> Index()
        {
            return View("Index", "Customer");
        }

        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Customers = await _crudService.GetAllAsync<List<CustomerGetVM>>("/customers");

            SetFullName();

            if (ViewBag.Role is null)
            {
                return RedirectToAction("AdminLogin", "Auth", new { area = "Admin" });
            }

            CashbackBalanceUpdateVM data = null;

            try
            {
                data = await _crudService.GetByIdAsync<CashbackBalanceUpdateVM>($"/CashbackBalances/{id}", id);
            }
            catch (Exception)
            {
                //TempData["Err"] = "CashbackBalance not found";
                return RedirectToAction("Index", "Customer");
            }

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, CashbackBalanceUpdateVM vm)
        {
            ViewBag.Customers = await _crudService.GetAllAsync<List<CustomerGetVM>>("/customers");

            try
            {
                await _crudService.Update($"/CashbackBalances/{id}", vm);
            }
            catch (Exception ex)
            {
                return View();
            }

            return RedirectToAction("Index", "Customer");
        }
    }
}
