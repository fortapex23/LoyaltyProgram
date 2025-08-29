using FluentValidation;
using LoyaltyConsole.Core.Enums;

namespace LoyaltyConsole.Business.DTOs.TransactionDtos
{
    public record TransactionUpdateDto(int CustomerId, decimal AmountSpent, BusinessTypes Business,
                                    decimal CashbackEarned, DateTime TransactionDate);

    public class TransactionUpdateDtoValidator : AbstractValidator<TransactionUpdateDto>
    {
        public TransactionUpdateDtoValidator()
        {
            RuleFor(x => x.CustomerId).NotNull().NotEmpty();
            RuleFor(x => x.AmountSpent).NotNull().NotEmpty();
            RuleFor(x => x.Business).NotNull();
            RuleFor(x => x.CashbackEarned).NotNull();
            RuleFor(x => x.TransactionDate).NotNull().NotEmpty();
        }
    }
}
