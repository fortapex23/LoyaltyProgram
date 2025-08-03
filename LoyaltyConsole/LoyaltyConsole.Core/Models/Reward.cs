namespace LoyaltyConsole.Core.Models
{
    public class Reward : BaseModel
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
