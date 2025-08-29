using System.ComponentModel.DataAnnotations;
using LoyaltyConsole.MVC.Enums;

namespace LoyaltyConsole.MVC.Areas.Admin.ViewModels.AuthVMs
{
    public class UserRegisterVM
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public GenderType Gender { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password length must be > 8")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "The password and confirm password dont match.")]
        public string ConfirmPassword { get; set; }
        public AdminStatus Status { get; set; } = AdminStatus.Pending;
    }
}
