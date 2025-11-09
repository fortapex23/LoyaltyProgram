using LoyaltyConsole.Core.Models;
using LoyaltyConsole.Core.Repositories;

namespace LoyaltyConsole.Data.Repositories
{
    public class CashbackBalanceRepository : GenericRepository<CashbackBalance>, ICashbackBalanceRepository
    {
        public CashbackBalanceRepository(AppDbContext context) : base(context)
        {
        }
    }
}
