using LoyaltyConsole.API.ApiResponses;
using LoyaltyConsole.Business.DTOs.RewardDtos;
using LoyaltyConsole.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyConsole.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RewardsController : ControllerBase
    {
        private readonly IRewardService _rewardService;

        public RewardsController(IRewardService rewardService)
        {
            _rewardService = rewardService;
        }

        [HttpGet("isexist/{id}")]
        public async Task<IActionResult> IsExist(int id)
        {
            bool exists = false;
            try
            {
                exists = await _rewardService.IsExist(f => f.Id == id);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }

            return Ok(new ApiResponse<bool>
            {
                Data = exists,
                StatusCode = StatusCodes.Status200OK
            });
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(new ApiResponse<ICollection<RewardGetDto>>
            {
                Data = await _rewardService.GetByExpression(true, null),
                ErrorMessage = null,
                StatusCode = StatusCodes.Status200OK
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(RewardCreateDto dto)
        {
            RewardGetDto reward = null;
            try
            {
                reward = await _rewardService.CreateAsync(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }

            return Created();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            RewardGetDto dto = null;
            try
            {
                dto = await _rewardService.GetSingleByExpression(true, x => x.Id == id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return Ok(new ApiResponse<RewardGetDto>
            {
                Data = dto,
                StatusCode = StatusCodes.Status200OK
            });
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, RewardUpdateDto dto)
        //{

        //    try
        //    {
        //        await _rewardService.UpdateAsync(id, dto);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new ApiResponse<RewardUpdateDto>
        //        {
        //            StatusCode = StatusCodes.Status400BadRequest,
        //            ErrorMessage = ex.Message,
        //            Data = null
        //        });
        //    }
        //    return NoContent();
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _rewardService.DeleteAsync(id);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            return Ok();
        }
    }
}
