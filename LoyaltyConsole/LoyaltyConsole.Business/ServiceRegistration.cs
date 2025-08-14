using LoyaltyConsole.Business.ExternalServices.Implementations;
using LoyaltyConsole.Business.ExternalServices.Interfaces;
using LoyaltyConsole.Business.Implementations;
using LoyaltyConsole.Business.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LoyaltyConsole.Business
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ICashbackBalanceService, CashbackBalanceService>();
            services.AddScoped<ICashbackService, CashbackService>();

        }
    }
}
