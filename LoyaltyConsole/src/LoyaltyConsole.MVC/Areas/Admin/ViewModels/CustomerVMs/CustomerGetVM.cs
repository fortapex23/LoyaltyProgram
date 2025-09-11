using LoyaltyConsole.MVC.Areas.Admin.ViewModels.CashbackBalanceVMs;
using LoyaltyConsole.MVC.Areas.Admin.ViewModels.CustomerImageVMs;
using LoyaltyConsole.MVC.Areas.Admin.ViewModels.TransactionVMs;

namespace LoyaltyConsole.MVC.Areas.Admin.ViewModels.CustomerVMs
{
    public class CustomerGetVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int CashbackBalanceId { get; set; }
        //public string? ImageUrl { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime CreatedDate { get; set; }

        public CustomerImageGetVM? CustomerImage { get; set; }
        public CashbackBalanceGetVM CashbackBalance { get; set; }
        public List<TransactionGetVM> Transactions { get; set; }
    }
}
