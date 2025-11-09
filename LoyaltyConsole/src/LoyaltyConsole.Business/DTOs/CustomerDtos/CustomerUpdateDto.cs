using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace LoyaltyConsole.Business.DTOs.CustomerDtos
{
    public record CustomerUpdateDto(string FullName, DateTime Birthday, IFormFile? ImageFile);

    public class CustomerUpdateDtoValidator : AbstractValidator<CustomerUpdateDto>
    {
        public CustomerUpdateDtoValidator()
        {
            RuleFor(x => x.FullName).NotNull().NotEmpty();
            RuleFor(x => x.Birthday).NotNull().NotEmpty();
        }
    }
}
