using LoyaltyConsole.Business.DTOs.TokenDtos;
using LoyaltyConsole.Business.DTOs.UserDtos;
using LoyaltyConsole.Core.Enums;

namespace LoyaltyConsole.Business.Interfaces
{
    public interface IAuthService
    {
        Task Register(UserRegisterDto dto);
        Task<TokenResponseDto> AdminLogin(UserLoginDto dto);
        Task<ICollection<UserGetDto>> GetAllUsersAsync();
        Task<ICollection<UserGetDto>> GetAllAdminsAsync();
        //Task UpdateStatusAsync(string id, AdminStatus status);
        Task<UserGetDto> GetById(string id);
        Task UpdateUserAsync(string id, UserEditDto dto);

        //Task<TokenResponseDto> Login(UserLoginDto dto);
        //Task ForgotPassword(ForgotPasswordDto dto);
    }
}
