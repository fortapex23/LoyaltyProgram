using Microsoft.AspNetCore.Http;

namespace LoyaltyConsole.Business.DTOs.CustomerDtos
{
    public record CustomerCreateDto(string FullName, DateTime Birthday, IFormFile Image);
}
