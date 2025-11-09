using LoyaltyConsole.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace LoyaltyConsole.Core.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }           
        public DateTime Birthday { get; set; }
        public GenderType Gender { get; set; }
        public AdminStatus Status { get; set; }
    }
}
