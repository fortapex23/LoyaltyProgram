namespace LoyaltyConsole.Core.Models
{
    public class Reward : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<AppUserReward> AppUserRewards { get; set; }
    }
}
