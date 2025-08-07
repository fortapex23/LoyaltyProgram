using FluentValidation;

namespace LoyaltyConsole.Business.DTOs.CustomerTagDtos
{
    public record CustomerTagUpdateDto(string TagName, string AppUserId);

    public class CustomerTagUpdateDtoValidator : AbstractValidator<CustomerTagUpdateDto>
    {
        public CustomerTagUpdateDtoValidator()
        {
            RuleFor(x => x.TagName).NotNull().NotEmpty();
            RuleFor(x => x.AppUserId).NotNull().NotEmpty();
        }
    }
}
