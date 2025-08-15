namespace LoyaltyConsole.Core.Models
{
    public class CashbackBalance : BaseModel
    {
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }

        public decimal TotalCashback { get; set; }
        public decimal CashbackRedeemed { get; set; }

        public decimal CashbackAvailable => TotalCashback - CashbackRedeemed;
    }
}
