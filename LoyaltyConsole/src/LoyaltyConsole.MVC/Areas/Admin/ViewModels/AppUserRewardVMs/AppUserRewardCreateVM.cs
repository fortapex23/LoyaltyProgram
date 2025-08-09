using System.ComponentModel.DataAnnotations;

namespace LoyaltyConsole.MVC.Areas.Admin.ViewModels.AppUserRewardVMs
{
    public class AppUserRewardCreateVM
    {
        public string AppUserId { get; set; }
        public int RewardId { get; set; }
    }
}
