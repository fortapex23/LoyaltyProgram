using LoyaltyConsole.MVC.Areas.Admin.ViewModels.CustomerVMs;
using LoyaltyConsole.MVC.Enums;

namespace LoyaltyConsole.MVC.Areas.Admin.ViewModels.TransactionVMs
{
    public class TransactionCreateVM
    {
        public int CustomerId { get; set; }
        public decimal AmountSpent { get; set; }
        public BusinessTypes Business { get; set; }
        public DateTime TransactionDate { get; set; }
        //public decimal CashbackEarned { get; set; }

        public CustomerGetVM Customer{ get; set; }
    }
}
