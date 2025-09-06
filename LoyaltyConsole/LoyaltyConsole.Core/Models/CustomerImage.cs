namespace LoyaltyConsole.Core.Models
{
    public class CustomerImage : BaseModel
    {
        public string ImageUrl { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }

}
