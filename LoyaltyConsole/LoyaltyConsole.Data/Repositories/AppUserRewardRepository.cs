using LoyaltyConsole.Core.Models;
using LoyaltyConsole.Core.Repositories;

namespace LoyaltyConsole.Data.Repositories
{
    public class AppUserRewardRepository : GenericRepository<AppUserReward>, IAppUserRewardRepository
    {
        public AppUserRewardRepository(AppDbContext context) : base(context)
        {
        }
    }
}
