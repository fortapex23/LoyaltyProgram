using LoyaltyConsole.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoyaltyConsole.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Birthday)
                .IsRequired();

            builder.Property(u => u.Gender)
                .IsRequired();

            builder.HasOne(u => u.CashbackBalance)
                .WithOne(cb => cb.AppUser)
                .HasForeignKey<CashbackBalance>(cb => cb.AppUserId);

            builder.HasMany(u => u.Transactions)
                .WithOne(t => t.AppUser)
                .HasForeignKey(t => t.AppUserId);
        }
    }
}
