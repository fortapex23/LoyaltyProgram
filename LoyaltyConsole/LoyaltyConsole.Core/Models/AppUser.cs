using LoyaltyConsole.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace LoyaltyConsole.Core.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }           
        public DateTime Birthday { get; set; }
        public GenderType Gender { get; set; }

        public List<CustomerTag> Tags { get; set; }
        public List<Transaction> Transactions { get; set; }
        public CashbackBalance CashbackBalance { get; set; }
        public List<AppUserReward> AppUserRewards { get; set; }
    }
}
