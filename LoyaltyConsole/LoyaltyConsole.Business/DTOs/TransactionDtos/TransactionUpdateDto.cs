using FluentValidation;
using LoyaltyConsole.Core.Enums;

namespace LoyaltyConsole.Business.DTOs.TransactionDtos
{
    public record TransactionUpdateDto(string AppUserId, decimal AmountSpent, BusinessTypes Business,
                                    decimal CashbackEarned);

    public class TransactionUpdateDtoValidator : AbstractValidator<TransactionUpdateDto>
    {
        public TransactionUpdateDtoValidator()
        {
            RuleFor(x => x.AppUserId).NotNull().NotEmpty();
            RuleFor(x => x.AmountSpent).NotNull().NotEmpty();
            RuleFor(x => x.Business).NotNull().NotEmpty();
            RuleFor(x => x.CashbackEarned).NotNull().NotEmpty();
        }
    }
}
