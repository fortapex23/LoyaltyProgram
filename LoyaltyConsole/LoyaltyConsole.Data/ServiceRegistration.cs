using LoyaltyConsole.Core.Repositories;
using LoyaltyConsole.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LoyaltyConsole.Data
{
    public static class ServiceRegistration
    {
        public static void AddRepositories(this IServiceCollection services, string connectionstring)
        {
            services.AddScoped<ICashbackBalanceRepository, CashbackBalanceRepository>();
            services.AddScoped<ICustomerTagRepository, CustomerTagRepository>();
            services.AddScoped<IRewardRepository, RewardRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IAppUserRewardRepository, AppUserRewardRepository>();

            services.AddDbContext<AppDbContext>(op =>
            {
                op.UseSqlServer(connectionstring);
            });
        }
    }
}
