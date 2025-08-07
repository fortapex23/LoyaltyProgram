using FluentValidation;

namespace LoyaltyConsole.Business.DTOs.RewardDtos
{
    public record RewardCreateDto(string Name, string Description);

    public class RewardCreateDtoValidator : AbstractValidator<RewardCreateDto>
    {
        public RewardCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Description).NotNull().NotEmpty();
        }
    }
}
