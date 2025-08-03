namespace LoyaltyConsole.Core.Models
{
    public class CustomerTag : BaseModel
    {
        public string TagName { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
