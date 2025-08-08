using LoyaltyConsole.Business.DTOs.TokenDtos;
using LoyaltyConsole.Business.DTOs.UserDtos;

namespace LoyaltyConsole.Business.Interfaces
{
    public interface IAuthService
    {
        Task Register(UserRegisterDto dto);
        Task<TokenResponseDto> AdminLogin(UserLoginDto dto);
        Task<ICollection<UserGetDto>> GetAllUsersAsync();
        Task<UserGetDto> GetById(string id);

        //Task<TokenResponseDto> Login(UserLoginDto dto);
        //Task UpdateUserAsync(string id, UserEditDto dto);
        //Task ForgotPassword(ForgotPasswordDto dto);
    }
}
