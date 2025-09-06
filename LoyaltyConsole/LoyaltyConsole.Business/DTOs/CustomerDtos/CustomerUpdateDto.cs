using Microsoft.AspNetCore.Http;

namespace LoyaltyConsole.Business.DTOs.CustomerDtos
{
    public record CustomerUpdateDto(string FullName, DateTime Birthday, IFormFile? Image);
}
