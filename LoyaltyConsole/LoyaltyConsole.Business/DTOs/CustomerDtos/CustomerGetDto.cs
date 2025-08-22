namespace LoyaltyConsole.Business.DTOs.CustomerDtos
{
    public record CustomerGetDto(int Id, string FullName, DateTime Birthday, DateTime CreatedDate);
}
