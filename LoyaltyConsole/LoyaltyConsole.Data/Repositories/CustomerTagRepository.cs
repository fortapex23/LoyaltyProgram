using LoyaltyConsole.Core.Models;
using LoyaltyConsole.Core.Repositories;

namespace LoyaltyConsole.Data.Repositories
{
    public class CustomerTagRepository : GenericRepository<CustomerTag>, ICustomerTagRepository
    {
        public CustomerTagRepository(AppDbContext context) : base(context)
        {
        }
    }
}
