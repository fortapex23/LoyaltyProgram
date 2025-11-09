using LoyaltyConsole.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoyaltyConsole.Data.Configurations
{
    public class CashbackBalanceConfiguration : IEntityTypeConfiguration<CashbackBalance>
    {
        public void Configure(EntityTypeBuilder<CashbackBalance> builder)
        {
            builder.Property(cb => cb.TotalCashback)
                .HasColumnType("decimal(18,2)");

            builder.Property(cb => cb.CashbackRedeemed)
                .HasColumnType("decimal(18,2)");

        }
    }
}
