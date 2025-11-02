using FluentValidation;
using LoyaltyConsole.Core.Enums;

namespace LoyaltyConsole.Business.DTOs.TransactionDtos
{
    public record TransactionCreateDto(int CustomerId, decimal AmountSpent, BusinessTypes Business, Currency Currency, DateTime TransactionDate);
                                    //decimal CashbackEarned

    public class TransactionCreateDtoValidator : AbstractValidator<TransactionCreateDto>
    {
        public TransactionCreateDtoValidator()
        {
            RuleFor(x => x.CustomerId).NotNull().NotEmpty();
            RuleFor(x => x.AmountSpent).NotNull().NotEmpty();
            RuleFor(x => x.Business).NotNull();
            RuleFor(x => x.Currency).NotNull();
            RuleFor(x => x.TransactionDate).NotNull().NotEmpty();
            //RuleFor(x => x.CashbackEarned).NotNull().NotEmpty();
        }
    }
}
