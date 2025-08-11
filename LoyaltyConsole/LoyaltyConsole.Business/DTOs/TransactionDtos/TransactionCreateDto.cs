using FluentValidation;
using LoyaltyConsole.Core.Enums;

namespace LoyaltyConsole.Business.DTOs.TransactionDtos
{
    public record TransactionCreateDto(string AppUserId, decimal AmountSpent, BusinessTypes Business, 
                                    decimal CashbackEarned);

    public class TransactionCreateDtoValidator : AbstractValidator<TransactionCreateDto>
    {
        public TransactionCreateDtoValidator()
        {
            RuleFor(x => x.AppUserId).NotNull().NotEmpty();
            RuleFor(x => x.AmountSpent).NotNull().NotEmpty();
            RuleFor(x => x.Business).NotNull().NotEmpty();
            RuleFor(x => x.CashbackEarned).NotNull().NotEmpty();
        }
    }
}
