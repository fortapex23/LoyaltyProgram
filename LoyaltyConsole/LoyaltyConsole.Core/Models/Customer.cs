using LoyaltyConsole.Core.Enums;

namespace LoyaltyConsole.Core.Models
{
    public class Customer : BaseModel
    {
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }

        public List<Transaction> Transactions { get; set; }
        public CashbackBalance CashbackBalance { get; set; }

        public CustomerImage CustomerImage { get; set; }
    }
}
