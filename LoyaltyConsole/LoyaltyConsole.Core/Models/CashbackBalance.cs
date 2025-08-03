namespace LoyaltyConsole.Core.Models
{
    public class CashbackBalance : BaseModel
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public decimal TotalCashback { get; set; }
        public decimal CashbackRedeemed { get; set; }

        public decimal CashbackAvailable => TotalCashback - CashbackRedeemed;
    }
}
