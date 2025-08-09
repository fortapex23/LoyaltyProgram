namespace LoyaltyConsole.MVC.Areas.Admin.ViewModels.CashbackBalanceVMs
{
    public class CashbackBalanceUpdateVM
    {
        public string AppUserId { get; set; }
        public decimal TotalCashback { get; set; }
        public decimal CashbackRedeemed { get; set; }
    }
}
