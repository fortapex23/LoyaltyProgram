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

        // No own index → redirect to Customers
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Customer");
        }

        // ---------------- UPDATE ----------------

        public async Task<IActionResult> Update(int id)
        {
            SetFullName();

            if (ViewBag.Role is null)
                return RedirectToAction("AdminLogin", "Auth", new { area = "Admin" });

            ViewBag.Customers =
                await _crudService.GetAsync<List<CustomerGetVM>>("/customers");

            try
            {
                var data =
                    await _crudService.GetAsync<CashbackBalanceUpdateVM>(
                        $"/cashbackbalances/{id}");

                return View(data);
            }
            catch
            {
                TempData["Error"] = "Cashback balance not found";
                return RedirectToAction("Index", "Customer");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, CashbackBalanceUpdateVM vm)
        {
            SetFullName();

            if (!ModelState.IsValid)
            {
                ViewBag.Customers =
                    await _crudService.GetAsync<List<CustomerGetVM>>("/customers");
                return View(vm);
            }

            try
            {
                await _crudService.UpdateAsync($"/cashbackbalances/{id}", vm);
            }
            catch
            {
                ModelState.AddModelError("", "Update failed");
                return View(vm);
            }

            return RedirectToAction("Index", "Customer");
        }
    }
}
