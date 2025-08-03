namespace LoyaltyConsole.Core.Models
{
    public class Transaction : BaseModel
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public decimal AmountSpent { get; set; }
        public string Business { get; set; }
        public decimal CashbackEarned { get; set; }

        public int? RewardId { get; set; }
        public Reward? Reward { get; set; }
    }
}
