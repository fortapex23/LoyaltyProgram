using FluentValidation;
using LoyaltyConsole.Business.DTOs.TransactionDtos;
using Microsoft.AspNetCore.Http;

namespace LoyaltyConsole.Business.DTOs.CustomerDtos
{
    public record CustomerCreateDto(string FullName, DateTime Birthday, IFormFile? ImageFile);

    public class CustomerCreateDtoValidator : AbstractValidator<CustomerCreateDto>
    {
        public CustomerCreateDtoValidator()
        {
            RuleFor(x => x.FullName).NotNull().NotEmpty();
            RuleFor(x => x.Birthday).NotNull().NotEmpty();
        }
    }
}
