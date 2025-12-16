using LoyaltyConsole.API.ApiResponses;
using LoyaltyConsole.Business.DTOs.CashbackBalanceDtos;
using LoyaltyConsole.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyConsole.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashbackBalancesController : ControllerBase
    {
        private readonly ICashbackBalanceService _cashbackBalanceService;

        public CashbackBalancesController(ICashbackBalanceService cashbackBalanceService)
        {
            _cashbackBalanceService = cashbackBalanceService;
        }

        [HttpGet("isexist/{id}")]
        public async Task<IActionResult> IsExist(int id)
        {
            var exists = await _cashbackBalanceService.IsExist(x => x.Id == id);

            return Ok(new ApiResponse<bool>
            {
                Data = exists,
                StatusCode = StatusCodes.Status200OK
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _cashbackBalanceService.GetByExpression(true, null);

            return Ok(new ApiResponse<ICollection<CashbackBalanceGetDto>>
            {
                Data = data,
                StatusCode = StatusCodes.Status200OK
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CashbackBalanceCreateDto dto)
        {
            var cashbackBalance = await _cashbackBalanceService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = cashbackBalance.Id },
                new ApiResponse<CashbackBalanceGetDto>
                {
                    Data = cashbackBalance,
                    StatusCode = StatusCodes.Status201Created
                }
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cashbackBalance = await _cashbackBalanceService
                .GetSingleByExpression(true, x => x.Id == id);

            return Ok(new ApiResponse<CashbackBalanceGetDto>
            {
                Data = cashbackBalance,
                StatusCode = StatusCodes.Status200OK
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CashbackBalanceUpdateDto dto)
        {
            await _cashbackBalanceService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _cashbackBalanceService.DeleteAsync(id);
            return NoContent();
        }
    }
}
