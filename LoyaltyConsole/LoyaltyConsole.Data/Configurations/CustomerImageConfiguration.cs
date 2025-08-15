using LoyaltyConsole.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoyaltyConsole.Data.Configurations
{
    public class CustomerImageConfiguration : IEntityTypeConfiguration<CustomerImage>
    {
        public void Configure(EntityTypeBuilder<CustomerImage> builder)
        {
            builder.Property(t => t.ImageUrl)
                .IsRequired();
        }
    }
}
