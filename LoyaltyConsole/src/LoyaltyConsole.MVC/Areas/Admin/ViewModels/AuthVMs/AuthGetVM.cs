using LoyaltyConsole.MVC.Enums;

namespace LoyaltyConsole.MVC.Areas.Admin.ViewModels.AuthVMs
{
    public record AuthGetVM(string AppUserId, string FullName, string Email, string PhoneNumber, AdminStatus Status,
                            DateTime BirthDay, GenderType Gender);
}
