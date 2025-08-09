using LoyaltyConsole.MVC.Enums;

namespace LoyaltyConsole.MVC.Areas.Admin.ViewModels.TransactionVMs
{
    public class TransactionGetVM
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public decimal AmountSpent { get; set; }
        public BusinessTypes Business { get; set; }
        public decimal CashbackEarned { get; set; }
        public int? RewardId { get; set; }
    }
}
