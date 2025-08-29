namespace LoyaltyConsole.MVC.Areas.Admin.ViewModels.AuthVMs
{
    public record LoginResponseVM(string AccessToken, DateTime ExpireDate, string ErrorMessage);
}
