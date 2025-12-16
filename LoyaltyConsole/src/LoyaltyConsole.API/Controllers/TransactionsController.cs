using LoyaltyConsole.API.ApiResponses;
using LoyaltyConsole.Business.DTOs.TransactionDtos;
using LoyaltyConsole.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyConsole.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("isexist/{id}")]
        public async Task<IActionResult> IsExist(int id)
        {
            var exists = await _transactionService.IsExist(x => x.Id == id);

            return Ok(new ApiResponse<bool>
            {
                Data = exists,
                StatusCode = StatusCodes.Status200OK
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transactions = await _transactionService
                .GetByExpression(true, null, "Customer");

            return Ok(new ApiResponse<ICollection<TransactionGetDto>>
            {
                Data = transactions,
                StatusCode = StatusCodes.Status200OK
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransactionCreateDto dto)
        {
            var transaction = await _transactionService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = transaction.Id },
                new ApiResponse<TransactionGetDto>
                {
                    Data = transaction,
                    StatusCode = StatusCodes.Status201Created
                }
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transaction = await _transactionService
                .GetSingleByExpression(true, x => x.Id == id, "Customer");

            return Ok(new ApiResponse<TransactionGetDto>
            {
                Data = transaction,
                StatusCode = StatusCodes.Status200OK
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TransactionUpdateDto dto)
        {
            await _transactionService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _transactionService.DeleteAsync(id);
            return NoContent();
        }
    }
}
