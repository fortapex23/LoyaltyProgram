using LoyaltyConsole.Core.Enums;

namespace LoyaltyConsole.Core.Models
{
    public class Transaction : BaseModel
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public decimal AmountSpent { get; set; }
        public BusinessTypes Business { get; set; }
        public decimal CashbackEarned { get; set; }

    }
}
