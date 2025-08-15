using LoyaltyConsole.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoyaltyConsole.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasOne(u => u.CashbackBalance)
                .WithOne(cb => cb.Customer)
                .HasForeignKey<CashbackBalance>(cb => cb.CustomerId);

            builder.HasMany(u => u.Transactions)
                .WithOne(t => t.Customer)
                .HasForeignKey(t => t.CustomerId);
        }
    }
}
