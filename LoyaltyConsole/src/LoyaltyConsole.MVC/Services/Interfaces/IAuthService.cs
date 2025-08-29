using LoyaltyConsole.MVC.Areas.Admin.ViewModels.AuthVMs;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace LoyaltyConsole.MVC.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseVM> Login(UserLoginVM vm);
        Task<LoginResponseVM> AdminLogin(UserLoginVM vm);
        Task<bool> Update(string endpoint, object body = null);
        void Logout();
        Task<bool> Register(UserRegisterVM vm);
        //Task<string> ForgotPassword(ForgotPasswordVM vm);
    }
}
