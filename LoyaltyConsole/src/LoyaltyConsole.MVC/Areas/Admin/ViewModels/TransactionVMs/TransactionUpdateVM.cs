using LoyaltyConsole.MVC.Enums;

namespace LoyaltyConsole.MVC.Areas.Admin.ViewModels.TransactionVMs
{
    public class TransactionUpdateVM
    {
        public int CustomerId { get; set; }
        public decimal AmountSpent { get; set; }
        public BusinessTypes Business { get; set; }
        public decimal CashbackEarned { get; set; }
    }
}
