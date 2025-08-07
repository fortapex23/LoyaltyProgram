namespace LoyaltyConsole.Core.Models
{
    public class AppUserReward : BaseModel
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int RewardId { get; set; }
        public Reward Reward { get; set; }
    }
}
