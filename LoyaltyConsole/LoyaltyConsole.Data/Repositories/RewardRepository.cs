using LoyaltyConsole.Core.Models;
using LoyaltyConsole.Core.Repositories;

namespace LoyaltyConsole.Data.Repositories
{
    public class RewardRepository : GenericRepository<Reward>, IRewardRepository
    {
        public RewardRepository(AppDbContext context) : base(context)
        {
        }
    }
}
