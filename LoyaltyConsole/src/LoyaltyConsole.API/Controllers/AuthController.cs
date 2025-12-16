using LoyaltyConsole.Business.DTOs.TokenDtos;
using LoyaltyConsole.Business.DTOs.UserDtos;
using LoyaltyConsole.Business.Interfaces;
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

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("AdminLogin")]
        public async Task<IActionResult> AdminLogin(UserLoginDto dto)
        {
            var token = await _authService.AdminLogin(dto);
            return Ok(token);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _authService.GetAllUsersAsync());
        }

        [HttpGet("Admins")]
        public async Task<IActionResult> GetAllAdmins()
        {
            return Ok(await _authService.GetAllAdminsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _authService.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UserEditDto dto)
        {
            await _authService.UpdateUserAsync(id, dto);
            return NoContent();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            await _authService.Register(dto);
            return Ok("Registration successful");
        }
    }
}
