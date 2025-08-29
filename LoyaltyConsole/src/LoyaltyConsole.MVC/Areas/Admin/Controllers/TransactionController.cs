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

        public async Task<IActionResult> Index()
        {
            SetFullName();

            if (ViewBag.Role is null)
            {
                return RedirectToAction("AdminLogin", "Auth", new { area = "Admin" });
            }

            var transactions = await _crudService.GetAllAsync<List<TransactionGetVM>>("/transactions");
            var customers = await _crudService.GetAllAsync<List<CustomerGetVM>>("/customers");

            foreach (var tx in transactions)
            {
                tx.Customer = customers.FirstOrDefault(c => c.Id == tx.CustomerId);
            }

            return View(transactions);
        }


        public async Task<IActionResult> Create()
        {
            SetFullName();

            if (ViewBag.Role is null)
            {
                return RedirectToAction("AdminLogin", "Auth", new { area = "Admin" });
            }

            ViewBag.Customers = await _crudService.GetAllAsync<List<CustomerGetVM>>("/customers");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransactionCreateVM vm)
        {
            try
            {
                await _crudService.Create("/transactions", vm);
            }
            catch (Exception ex)
            {
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            SetFullName();

            if (ViewBag.Role is null)
            {
                return RedirectToAction("AdminLogin", "Auth", new { area = "Admin" });
            }

            try
            {
                await _crudService.Delete<object>($"/transactions/{id}", id);
            }
            catch (Exception ex)
            {
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            SetFullName();

            if (ViewBag.Role is null)
            {
                return RedirectToAction("AdminLogin", "Auth", new { area = "Admin" });
            }

            ViewBag.Customers = await _crudService.GetAllAsync<List<CustomerGetVM>>("/customers");

            TransactionUpdateVM data = null;

            try
            {
                data = await _crudService.GetByIdAsync<TransactionUpdateVM>($"/Transactions/{id}", id);
            }
            catch (Exception)
            {
                //TempData["Err"] = "Transaction not found";
                return RedirectToAction("Index");
            }

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, TransactionUpdateVM vm)
        {
            ViewBag.Customers = await _crudService.GetAllAsync<List<CustomerGetVM>>("/customers");

            try
            {
                await _crudService.Update($"/Transactions/{id}", vm);
            }
            catch (Exception ex)
            {
                return View();
            }

            return RedirectToAction("Index");
        }
    }
}
