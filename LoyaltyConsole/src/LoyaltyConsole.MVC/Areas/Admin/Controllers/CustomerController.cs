using LoyaltyConsole.MVC.Areas.Admin.Controllers;
using LoyaltyConsole.MVC.Areas.Admin.PaginatedLists;
using LoyaltyConsole.MVC.Areas.Admin.ViewModels.CustomerVMs;
using LoyaltyConsole.MVC.Exceptions;
using LoyaltyConsole.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[Area("Admin")]
public class CustomerController : BaseController
{
    private readonly ICrudService _crudService;

    public CustomerController(ICrudService crudService)
    {
        _crudService = crudService;
    }

    public async Task<IActionResult> Index(int page = 1, string? search = null)
    {
        SetFullName();

        if (ViewBag.Role is null)
            return RedirectToAction("AdminLogin", "Auth", new { area = "Admin" });

        var endpoint = search != null
            ? $"/api/customers?page={page}&pageSize=5&search={search}"
            : $"/api/customers?page={page}&pageSize=5";

        var customers = await _crudService.GetAsync<PagedResult<CustomerListVM>>(endpoint);

        ViewBag.Search = search;
        return View(customers);
    }


    public IActionResult Create()
    {
        SetFullName();

        if (ViewBag.Role is null)
            return RedirectToAction("AdminLogin", "Auth", new { area = "Admin" });

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CustomerCreateVM vm)
    {
        if (!ModelState.IsValid) return View(vm);

        try
        {
            await _crudService.CreateWithImageAsync("customers", vm);
        }
        catch (ApiValidationException ex)
        {
            ModelState.AddModelError(nameof(vm.Birthday), ex.Message);
            return View(vm);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _crudService.DeleteAsync($"customers/{id}");
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int id)
    {
        var customer =
            await _crudService.GetAsync<CustomerUpdateVM>($"customers/{id}");

        return View(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, CustomerUpdateVM vm)
    {
        if (!ModelState.IsValid) return View(vm);

        await _crudService.UpdateWithImageAsync($"customers", id, vm);
        return RedirectToAction(nameof(Index));
    }
}
