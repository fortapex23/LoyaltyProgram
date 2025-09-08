namespace LoyaltyConsole.MVC.Areas.Admin.ViewModels.CustomerVMs
{
    public class CustomerUpdateVM
    {
        public string FullName { get; set; }
        public double Cashback { get; set; }
        public DateTime Birthday { get; set; }
        public IFormFile Image { get; set; }
    }
}
