using LoyaltyConsole.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoyaltyConsole.Data.Configurations
{
    public class CustomerTagConfiguration : IEntityTypeConfiguration<CustomerTag>
    {
        public void Configure(EntityTypeBuilder<CustomerTag> builder)
        {
            builder.Property(t => t.TagName)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(t => t.AppUser)
                .WithMany(u => u.Tags)
                .HasForeignKey(t => t.AppUserId);
        }
    }
}
