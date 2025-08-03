using Microsoft.AspNetCore.Identity;

namespace LoyaltyConsole.Core.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }           
        public DateTime Birthday { get; set; }

        public List<CustomerTag> Tags { get; set; }
        public List<Transaction> Transactions { get; set; } = new();
        public CashbackBalance CashbackBalance { get; set; }
        public List<Reward> Rewards { get; set; }
    }
}
