using LoyaltyConsole.MVC.Services.Implementations;
using LoyaltyConsole.MVC.Services.Interfaces;

namespace LoyaltyConsole.MVC
{
    public static class ServiceRegistration
    {
        public static void AddRegisterService(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICrudService, CrudService>();
        }
    }
}
