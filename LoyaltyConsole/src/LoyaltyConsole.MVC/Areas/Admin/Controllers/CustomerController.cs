using LoyaltyConsole.MVC.Areas.Admin.ViewModels.CustomerVMs;
using LoyaltyConsole.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyConsole.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : BaseController
    {
        private readonly ICrudService _crudService;

        public CustomerController(ICrudService crudService)
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

            var datas = await _crudService.GetAllAsync<List<CustomerGetVM>>("/customers");
            return View(datas);
        }

        public IActionResult Create()
        {
            SetFullName();

            if (ViewBag.Role is null)
            {
                return RedirectToAction("AdminLogin", "Auth", new { area = "Admin" });
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerCreateVM vm)
        {
            try
            {
                await _crudService.Create("/customers", vm);
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
                await _crudService.Delete<object>($"/customers/{id}", id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Customer Not Found";
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

            CustomerUpdateVM data = null;

            try
            {
                data = await _crudService.GetByIdAsync<CustomerUpdateVM>($"/Customers/{id}", id);
            }
            catch (Exception)
            {
                //TempData["Err"] = "Customer not found";
                return RedirectToAction("Index");
            }

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, CustomerUpdateVM vm)
        {
            try
            {
                await _crudService.Update($"/Customers/{id}", vm);
            }
            catch (Exception ex)
            {
                return View();
            }

            return RedirectToAction("Index");
        }
    }
}
