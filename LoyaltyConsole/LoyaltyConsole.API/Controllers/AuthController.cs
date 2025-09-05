using LoyaltyConsole.API.ApiResponses;
using LoyaltyConsole.Business.DTOs.TokenDtos;
using LoyaltyConsole.Business.DTOs.UserDtos;
using LoyaltyConsole.Business.Interfaces;
using LoyaltyConsole.Core.Enums;
using LoyaltyConsole.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyConsole.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public AuthController(IAuthService authService, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _authService = authService;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AdminLogin(UserLoginDto dto)
        {
            TokenResponseDto rDto = null;
            try
            {
                rDto = await _authService.AdminLogin(dto);
                return Ok(rDto);
            }
            catch (Exception ex) when (ex.Message == "Invalid credentials")
            {
                return BadRequest(new ApiResponse<string>
                {
                    Data = null,
                    ErrorMessage = "Invalid email or password",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            catch (Exception)
            {
                return BadRequest("An error occurred");
            }
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(new ApiResponse<ICollection<UserGetDto>>
            {
                Data = await _authService.GetAllUsersAsync(),
                ErrorMessage = null,
                StatusCode = StatusCodes.Status200OK
            });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAdmins()
        {
            return Ok(new ApiResponse<ICollection<UserGetDto>>
            {
                Data = await _authService.GetAllAdminsAsync(),
                ErrorMessage = null,
                StatusCode = StatusCodes.Status200OK
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            UserGetDto dto = null;
            try
            {
                dto = await _authService.GetById(id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return Ok(new ApiResponse<UserGetDto>
            {
                Data = dto,
                StatusCode = StatusCodes.Status200OK
            });
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(string id, AdminStatus status)
        {
            try
            {
                await _authService.UpdateStatusAsync(id, status);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UserEditDto dto)
        {
            try
            {
                await _authService.UpdateUserAsync(id, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<UserEditDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            return NoContent();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _authService.Register(dto);
            }
            catch (NullReferenceException)
            {
                return BadRequest("User creation failed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Registration successful");
        }

        //[HttpGet("")]
        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser appUser = await _userManager.FindByEmailAsync("admin@gmail.com");

        //    await _userManager.AddToRoleAsync(appUser, "Admin");

        //    return Ok();
        //}


        //[HttpGet("")]
        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole role = new IdentityRole("SuperAdmin");

        //    await _roleManager.CreateAsync(role);

        //    return Ok();
        //}

    }
}
