using LoyaltyConsole.API.ApiResponses;
using LoyaltyConsole.Business.DTOs.CustomerDtos;
using LoyaltyConsole.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyConsole.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string input)
        {
            var customers = await _customerService.SearchCustomer(input);

            return Ok(new ApiResponse<ICollection<CustomerListDto>>
            {
                Data = customers,
                StatusCode = StatusCodes.Status200OK
            });
        }

        [HttpGet("isexist/{id}")]
        public async Task<IActionResult> IsExist(int id)
        {
            bool exists = await _customerService.IsExist(x => x.Id == id);

            return Ok(new ApiResponse<bool>
            {
                Data = exists,
                StatusCode = StatusCodes.Status200OK
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerService.GetListAsync();

            return Ok(new ApiResponse<ICollection<CustomerListDto>>
            {
                Data = customers,
                StatusCode = StatusCodes.Status200OK
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CustomerCreateDto dto)
        {
            var customer = await _customerService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = customer.Id },
                new ApiResponse<CustomerGetDto>
                {
                    Data = customer,
                    StatusCode = StatusCodes.Status201Created
                }
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerService.GetSingleByExpression(
                true,
                x => x.Id == id,
                "CashbackBalance",
                "Transactions",
                "CustomerImage"
            );

            return Ok(new ApiResponse<CustomerGetDto>
            {
                Data = customer,
                StatusCode = StatusCodes.Status200OK
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CustomerUpdateDto dto)
        {
            await _customerService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerService.DeleteAsync(id);
            return NoContent();
        }
    }
}
