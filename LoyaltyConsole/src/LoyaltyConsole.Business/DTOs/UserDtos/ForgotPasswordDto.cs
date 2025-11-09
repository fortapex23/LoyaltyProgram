using FluentValidation;

namespace LoyaltyConsole.Business.DTOs.UserDtos
{
    public record ForgotPasswordDto(string Email, string Password, string ConfirmPassword);

    public class ForgotPasswordDtoValidator : AbstractValidator<ForgotPasswordDto>
    {
        public ForgotPasswordDtoValidator()
        {
            RuleFor(x => x.Password).MinimumLength(8).MaximumLength(40);


            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.Password != x.ConfirmPassword)
                {
                    context.AddFailure("ConfirmPassword", "pw and confirm pw dont match");
                }
            });
        }
    }
}
