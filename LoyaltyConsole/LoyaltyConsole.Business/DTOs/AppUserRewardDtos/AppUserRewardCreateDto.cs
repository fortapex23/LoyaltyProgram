using FluentValidation;

namespace LoyaltyConsole.Business.DTOs.AppUserRewardDtos
{
    public record AppUserRewardCreateDto(string AppUserId, int RewardId);

    public class AppUserRewardCreateDtoValidator : AbstractValidator<AppUserRewardCreateDto>
    {
        public AppUserRewardCreateDtoValidator()
        {
            RuleFor(x => x.AppUserId).NotNull().NotEmpty();
            RuleFor(x => x.RewardId).NotNull().NotEmpty();
        }
    }
}
