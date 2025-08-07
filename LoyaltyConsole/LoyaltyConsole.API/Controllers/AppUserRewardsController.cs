using LoyaltyConsole.API.ApiResponses;
using LoyaltyConsole.Business.DTOs.AppUserRewardDtos;
using LoyaltyConsole.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyConsole.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserRewardsController : ControllerBase
    {
        private readonly IAppUserRewardService _appUserRewardService;

        public AppUserRewardsController(IAppUserRewardService appUserRewardService)
        {
            _appUserRewardService = appUserRewardService;
        }

        [HttpGet("isexist/{id}")]
        public async Task<IActionResult> IsExist(int id)
        {
            bool exists = false;
            try
            {
                exists = await _appUserRewardService.IsExist(f => f.Id == id);
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
            return Ok(new ApiResponse<ICollection<AppUserRewardGetDto>>
            {
                Data = await _appUserRewardService.GetByExpression(true, null),
                ErrorMessage = null,
                StatusCode = StatusCodes.Status200OK
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppUserRewardCreateDto dto)
        {
            AppUserRewardGetDto appUserAppUserReward = null;
            try
            {
                appUserAppUserReward = await _appUserRewardService.CreateAsync(dto);
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
            AppUserRewardGetDto dto = null;
            try
            {
                dto = await _appUserRewardService.GetSingleByExpression(true, x => x.Id == id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return Ok(new ApiResponse<AppUserRewardGetDto>
            {
                Data = dto,
                StatusCode = StatusCodes.Status200OK
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AppUserRewardUpdateDto dto)
        {

            try
            {
                await _appUserRewardService.UpdateAsync(id, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AppUserRewardUpdateDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _appUserRewardService.DeleteAsync(id);
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
