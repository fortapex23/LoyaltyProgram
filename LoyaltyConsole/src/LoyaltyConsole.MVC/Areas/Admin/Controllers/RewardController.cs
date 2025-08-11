using LoyaltyConsole.MVC.Areas.Admin.ViewModels.RewardVMs;
using LoyaltyConsole.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyConsole.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RewardController : Controller
    {
        private readonly ICrudService _crudService;

        public RewardController(ICrudService crudService)
        {
            _crudService = crudService;
        }

        public async Task<IActionResult> Index()
        {
            var datas = await _crudService.GetAllAsync<List<RewardGetVM>>("/rewards");
            return View(datas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RewardCreateVM vm)
        {
            try
            {
                await _crudService.Create("/rewards", vm);
            }
            catch (Exception ex)
            {
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _crudService.Delete<object>($"/rewards/{id}", id);
            }
            catch (Exception ex)
            {
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            RewardUpdateVM data = null;

            try
            {
                data = await _crudService.GetByIdAsync<RewardUpdateVM>($"/Rewards/{id}", id);
            }
            catch (Exception)
            {
                //TempData["Err"] = "Reward not found";
                return RedirectToAction("Index");
            }

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, RewardUpdateVM vm)
        {
            try
            {
                await _crudService.Update($"/Rewards/{id}", vm);
            }
            catch (Exception ex)
            {
                return View();
            }

            return RedirectToAction("Index");
        }
    }
}
