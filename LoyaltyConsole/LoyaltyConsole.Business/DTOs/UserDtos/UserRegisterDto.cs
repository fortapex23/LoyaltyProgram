using FluentValidation;
using LoyaltyConsole.Core.Enums;

namespace LoyaltyConsole.Business.DTOs.UserDtos
{
    public record UserRegisterDto(string FullName, string Email, string Password, string ConfirmPassword,
					string PhoneNumber, DateTime Birthday, GenderType Gender, AdminStatus Status);

	public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
	{
        public UserRegisterDtoValidator()
        {
			RuleFor(x => x.FullName).NotNull().NotEmpty().MaximumLength(60);

			RuleFor(x => x.Password).MinimumLength(8).MaximumLength(40);

			RuleFor(x => x.Email).NotNull().NotEmpty();

			RuleFor(x => x.Birthday).NotEmpty().WithMessage("cant be empty")
				.LessThan(x => DateTime.Now).WithMessage("wrong birthday");



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
