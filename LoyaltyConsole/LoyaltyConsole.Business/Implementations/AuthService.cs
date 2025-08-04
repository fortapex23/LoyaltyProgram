using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LoyaltyConsole.Business.DTOs.TokenDtos;
using LoyaltyConsole.Business.DTOs.UserDtos;
using LoyaltyConsole.Business.Interfaces;
using LoyaltyConsole.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LoyaltyConsole.Business.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<ICollection<UserGetDto>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            var userDtos = users.Select(user => new UserGetDto(
                user.Id,
                user.FullName,
                user.Email,
                user.PhoneNumber,
                user.Birthday,
                user.Gender
            )).ToList();

            return userDtos;
        }

        public async Task<UserGetDto> GetById(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id.ToString());

            if (user == null)
            {
                throw new NullReferenceException($"{id} not found");
            }

            var userDto = new UserGetDto(
                user.Id,
                user.FullName,
                user.Email,
                user.PhoneNumber,
                user.Birthday,
                user.Gender
            );

            return userDto;
        }

        public async Task<TokenResponseDto> Login(UserLoginDto dto)
        {
            AppUser appUser = null;

            appUser = await _userManager.FindByEmailAsync(dto.Email);

            if (appUser == null)
            {
                throw new Exception("Invalid credentials");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(appUser, dto.Password, dto.RememberMe);

            if (!result.Succeeded)
            {
                throw new Exception("Invalid credentials");
            }

            var roles = await _userManager.GetRolesAsync(appUser);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, appUser.Id),
                new Claim(ClaimTypes.Name, appUser.FullName),
            };

            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));
            DateTime expiredt = DateTime.UtcNow.AddHours(1);
            string secretkey = _configuration.GetSection("JWT:secretKey").Value;

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claims,
                audience: _configuration.GetSection("JWT:audience").Value,
                issuer: _configuration.GetSection("JWT:issuer").Value,
                expires: expiredt,
                notBefore: DateTime.UtcNow
                );

            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return new TokenResponseDto(token, expiredt);
        }

        public async Task Register(UserRegisterDto dto)
        {
            AppUser appUser = new AppUser()
            {
                Email = dto.Email,
                Birthday = dto.Birthday,
                Gender = dto.Gender,
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                UserName = dto.FullName,
            };

            if (appUser.Birthday >= DateTime.Now)
                throw new Exception("Invalid birth date");

            var result = await _userManager.CreateAsync(appUser, dto.Password);

            if (!result.Succeeded)
            {
                throw new NullReferenceException();
            }

            var member = await _userManager.FindByEmailAsync(dto.Email);

            if (member is not null)
            {
                await _userManager.AddToRoleAsync(appUser, "Client");
            }
        }
    }
}
