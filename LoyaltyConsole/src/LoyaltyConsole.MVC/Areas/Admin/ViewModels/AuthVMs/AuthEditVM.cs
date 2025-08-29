using LoyaltyConsole.MVC.Enums;

namespace LoyaltyConsole.MVC.Areas.Admin.ViewModels.AuthVMs
{
    public record AuthEditVM(string FullName, string Email, string PassportNumber, string PhoneNumber,
                            DateTime BirthDate, GenderType Gender);
}
