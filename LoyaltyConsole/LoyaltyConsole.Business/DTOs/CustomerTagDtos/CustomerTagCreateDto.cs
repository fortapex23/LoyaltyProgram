using FluentValidation;

namespace LoyaltyConsole.Business.DTOs.CustomerTagDtos
{
    public record CustomerTagCreateDto(string TagName, string AppUserId);

    public class CustomerTagCreateDtoValidator : AbstractValidator<CustomerTagCreateDto>
    {
        public CustomerTagCreateDtoValidator()
        {
            RuleFor(x => x.TagName).NotNull().NotEmpty();
            RuleFor(x => x.AppUserId).NotNull().NotEmpty();
        }
    }
}
