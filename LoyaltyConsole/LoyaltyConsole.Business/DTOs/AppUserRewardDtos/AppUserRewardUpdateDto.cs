using FluentValidation;

namespace LoyaltyConsole.Business.DTOs.AppUserRewardDtos
{
    public record AppUserRewardUpdateDto(string AppUserId, int RewardId);

    public class AppUserRewardUpdateDtoValidator : AbstractValidator<AppUserRewardUpdateDto>
    {
        public AppUserRewardUpdateDtoValidator()
        {
            RuleFor(x => x.AppUserId).NotNull().NotEmpty();
            RuleFor(x => x.RewardId).NotNull().NotEmpty();
        }
    }
}
