namespace LoyaltyConsole.Business.DTOs.CustomerDtos
{
    public class CustomerListDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal TotalCashback { get; set; }
        public string ImageUrl { get; set; }
    }
}
