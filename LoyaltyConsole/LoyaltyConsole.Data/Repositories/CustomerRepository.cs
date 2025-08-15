using LoyaltyConsole.Core.Models;
using LoyaltyConsole.Core.Repositories;

namespace LoyaltyConsole.Data.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context)
        {
        }
    }
}
