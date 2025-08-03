using LoyaltyConsole.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoyaltyConsole.Data.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.Property(t => t.AmountSpent)
                .HasColumnType("decimal(18,2)");

            builder.Property(t => t.CashbackEarned)
                .HasColumnType("decimal(18,2)");

            builder.Property(t => t.Business)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
