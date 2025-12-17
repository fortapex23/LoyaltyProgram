using LoyaltyConsole.MVC.Areas.Admin.PaginatedLists;
using LoyaltyConsole.MVC.Areas.Admin.ViewModels.CustomerVMs;
using LoyaltyConsole.MVC.Areas.Admin.ViewModels.TransactionVMs;
using LoyaltyConsole.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyConsole.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TransactionController : BaseController
    {
        private readonly ICrudService _crudService;

        public TransactionController(ICrudService crudService)
        {
            _crudService = crudService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            SetFullName();

            if (ViewBag.Role is null)
                return RedirectToAction("AdminLogin", "Auth", new { area = "Admin" });

            int pageSize = 8;

            var endpoint = $"/transactions?page={page}&pageSize={pageSize}";

            var transactions =
                await _crudService.GetAsync<PagedResult<TransactionGetVM>>(endpoint);

            return View(transactions);
        }

        // ---------------- CREATE ----------------

        public async Task<IActionResult> Create()
        {
            SetFullName();

            if (ViewBag.Role is null)
                return RedirectToAction("AdminLogin", "Auth", new { area = "Admin" });

            ViewBag.Customers =
                await _crudService.GetAsync<List<CustomerGetVM>>("/customers");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransactionCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Customers =
                    await _crudService.GetAsync<List<CustomerGetVM>>("/customers");
                return View(vm);
            }

            try
            {
                await _crudService.CreateAsync("/transactions", vm);
            }
            catch
            {
                ModelState.AddModelError("", "Transaction creation failed");
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }

        // ---------------- DELETE ----------------

        public async Task<IActionResult> Delete(int id)
        {
            SetFullName();

            if (ViewBag.Role is null)
                return RedirectToAction("AdminLogin", "Auth", new { area = "Admin" });

            try
            {
                await _crudService.DeleteAsync($"/transactions/{id}");
            }
            catch
            {
                TempData["Error"] = "Transaction not found";
            }

            return RedirectToAction(nameof(Index));
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
                var transaction =
                    await _crudService.GetAsync<TransactionUpdateVM>($"/transactions/{id}");
                return View(transaction);
            }
            catch
            {
                TempData["Error"] = "Transaction not found";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, TransactionUpdateVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Customers =
                    await _crudService.GetAsync<List<CustomerGetVM>>("/customers");
                return View(vm);
            }

            try
            {
                await _crudService.UpdateAsync($"/transactions/{id}", vm);
            }
            catch
            {
                ModelState.AddModelError("", "Update failed");
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
