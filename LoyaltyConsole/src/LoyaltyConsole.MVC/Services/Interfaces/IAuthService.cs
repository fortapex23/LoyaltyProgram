using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace LoyaltyConsole.MVC.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseVM> Login(UserLoginVM vm);
        Task<LoginResponseVM> AdminLogin(UserLoginVM vm);
        void Logout();
        Task<bool> Register(UserRegisterVM vm);
        Task<string> ForgotPassword(ForgotPasswordVM vm);
    }
}
