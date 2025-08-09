namespace LoyaltyConsole.MVC.ViewModels.AuthVMs
{
    public record LoginResponseVM(string AccessToken, DateTime ExpireDate, string ErrorMessage);
}
