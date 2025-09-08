using LoyaltyConsole.MVC.Areas.Admin.ViewModels.CustomerVMs;
using LoyaltyConsole.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyConsole.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : BaseController
    {
        private readonly ICrudService _crudService;
        private readonly IWebHostEnvironment _env;

        public CustomerController(ICrudService crudService, IWebHostEnvironment env)
        {
            _crudService = crudService;
            _env = env;
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
            if (!ModelState.IsValid) return View(vm);

            try
            {
                // Build DTO without image (only simple props)
                var createDto = new
                {
                    vm.FullName,
                    vm.Birthday
                };

                // Call service and pass the IFormFile along
                await _crudService.CreateWithImage("/customers", createDto, vm.Image);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating customer: {ex.Message}");
                return View(vm);
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
            catch (Exception)
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
                data = await _crudService.GetByIdAsync<CustomerUpdateVM>($"/customers/{id}", id);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, CustomerUpdateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            try
            {
                string fileName = null;

                if (vm.Image != null)
                {
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.Image.FileName);
                    string path = Path.Combine(_env.WebRootPath, "uploads/customers", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await vm.Image.CopyToAsync(stream);
                    }
                }

                var updateDto = new
                {
                    vm.FullName,
                    vm.Birthday,
                    vm.Cashback,
                    Image = fileName // replace or keep old one depending on your API logic
                };

                await _crudService.Update($"/customers/{id}", updateDto);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Error updating customer");
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
