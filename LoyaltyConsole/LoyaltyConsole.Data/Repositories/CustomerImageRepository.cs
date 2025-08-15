using LoyaltyConsole.Core.Models;
using LoyaltyConsole.Core.Repositories;

namespace LoyaltyConsole.Data.Repositories
{
    public class CustomerImageRepository : GenericRepository<CustomerImage>, ICustomerImageRepository
    {
        public CustomerImageRepository(AppDbContext context) : base(context)
        {
        }
    }
}
