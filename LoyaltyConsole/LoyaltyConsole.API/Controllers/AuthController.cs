using LoyaltyConsole.API.ApiResponses;
using LoyaltyConsole.Business.DTOs.TokenDtos;
using LoyaltyConsole.Business.DTOs.UserDtos;
using LoyaltyConsole.Business.Interfaces;
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

        //[HttpGet("")]
        //public async Task<IActionResult> GetAll()
        //{
        //    return Ok(new ApiResponse<ICollection<UserGetDto>>
        //    {
        //        Data = await _authService.GetAllUsersAsync(),
        //        ErrorMessage = null,
        //        StatusCode = StatusCodes.Status200OK
        //    });
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(string id)
        //{
        //    UserGetDto dto = null;
        //    try
        //    {
        //        dto = await _authService.GetById(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound(ex.Message);
        //    }

        //    return Ok(new ApiResponse<UserGetDto>
        //    {
        //        Data = dto,
        //        StatusCode = StatusCodes.Status200OK
        //    });
        //}

        //[HttpPost("[action]")]
        //public async Task<IActionResult> Register(UserRegisterDto dto)
        //{
        //    try
        //    {
        //        await _authService.Register(dto);
        //    }
        //    catch (NullReferenceException)
        //    {
        //        return BadRequest();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //    return Ok();
        //}

        //[HttpPost("[action]")]
        //public async Task<IActionResult> Login(UserLoginDto dto)
        //{
        //    TokenResponseDto rDto = null;

        //    try
        //    {
        //        rDto = await _authService.Login(dto);
        //    }
        //    catch (Exception ex) when (ex.Message == "Invalid credentials")
        //    {
        //        return BadRequest(new ApiResponse<string>
        //        {
        //            Data = null,
        //            ErrorMessage = "Invalid email or password",
        //            StatusCode = StatusCodes.Status400BadRequest
        //        });
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest("An error occurred");
        //    }

        //    return Ok(rDto);
        //}

        //[HttpGet("")]
        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser appUser = await _userManager.FindByEmailAsync("admin@gmail.com");

        //    await _userManager.AddToRoleAsync(appUser, "Admin");

        //    return Ok();
        //}


        [HttpGet("")]
        public async Task<IActionResult> CreateRole()
        {
            IdentityRole role2 = new IdentityRole("Admin");
            IdentityRole role3 = new IdentityRole("Client");

            await _roleManager.CreateAsync(role2);
            await _roleManager.CreateAsync(role3);

            return Ok();
        }

    }
}
