namespace LoyaltyConsole.MVC.Areas.Admin.ViewModels.CustomerVMs
{
    public class CustomerListVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal TotalCashback { get; set; }
        public decimal CashbackRedeemed { get; set; }
        public decimal CashbackAvailable { get; set; }

        public string? ImageUrl { get; set; }
    }
}
