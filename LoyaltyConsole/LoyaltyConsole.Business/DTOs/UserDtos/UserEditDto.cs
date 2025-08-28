using LoyaltyConsole.Core.Enums;

namespace LoyaltyConsole.Business.DTOs.UserDtos
{
    public record UserEditDto(string FullName, string Email, string PhoneNumber, AdminStatus Status,
                                DateTime Birthday, GenderType Gender);
}
