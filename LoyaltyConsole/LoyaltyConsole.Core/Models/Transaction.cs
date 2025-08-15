using LoyaltyConsole.Core.Enums;

namespace LoyaltyConsole.Core.Models
{
    public class Transaction : BaseModel
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public decimal AmountSpent { get; set; }
        public BusinessTypes Business { get; set; }
        public decimal CashbackEarned { get; set; }

    }
}
