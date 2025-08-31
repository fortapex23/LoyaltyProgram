using LoyaltyConsole.MVC.Enums;

namespace LoyaltyConsole.MVC.Areas.Admin.ViewModels.AuthVMs
{
    public class AuthGetVM
    {
        public string AppUserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public AdminStatus Status { get; set; }
        public DateTime BirthDay { get; set; }
        public GenderType Gender { get; set; }
    }

}
