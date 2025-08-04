using FluentValidation;

namespace LoyaltyConsole.Business.DTOs.UserDtos
{
    public record UserLoginDto(string Email, string Password, bool RememberMe);

    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().MaximumLength(60);

            RuleFor(x => x.Password).NotEmpty().NotNull();
        }
    }
}
