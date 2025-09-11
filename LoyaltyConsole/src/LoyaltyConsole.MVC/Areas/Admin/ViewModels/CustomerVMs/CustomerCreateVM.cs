using System.ComponentModel.DataAnnotations;

namespace LoyaltyConsole.MVC.Areas.Admin.ViewModels.CustomerVMs
{
    public class CustomerCreateVM
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        public IFormFile Image { get; set; }
    }
}
