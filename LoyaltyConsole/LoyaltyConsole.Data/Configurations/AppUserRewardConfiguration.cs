using LoyaltyConsole.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoyaltyConsole.Data.Configurations
{
    public class AppUserRewardConfiguration : IEntityTypeConfiguration<AppUserReward>
    {
        public void Configure(EntityTypeBuilder<AppUserReward> builder)
        {
            builder.HasOne(ar => ar.AppUser)
                .WithMany(u => u.AppUserRewards)
                .HasForeignKey(ar => ar.AppUserId);

            builder.HasOne(ar => ar.Reward)
                   .WithMany(r => r.AppUserRewards)
                   .HasForeignKey(ar => ar.RewardId);
        }
    }
}
