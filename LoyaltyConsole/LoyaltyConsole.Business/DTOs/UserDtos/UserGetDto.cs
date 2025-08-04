using LoyaltyConsole.Core.Enums;

namespace LoyaltyConsole.Business.DTOs.UserDtos
{
    public record UserGetDto(string AppUserId, string FullName, string Email, string PhoneNumber,
                            DateTime Birthday, GenderType Gender);
}
