using FluentValidation;
using LoyaltyConsole.Core.Enums;

namespace LoyaltyConsole.Business.DTOs.TransactionDtos
{
    public record TransactionCreateDto(int CustomerId, decimal AmountSpent, BusinessTypes Business, DateTime TransactionDate);
                                    //decimal CashbackEarned

    public class TransactionCreateDtoValidator : AbstractValidator<TransactionCreateDto>
    {
        public TransactionCreateDtoValidator()
        {
            RuleFor(x => x.CustomerId).NotNull().NotEmpty();
            RuleFor(x => x.AmountSpent).NotNull().NotEmpty();
            RuleFor(x => x.Business).NotNull().NotEmpty();
            RuleFor(x => x.TransactionDate).NotNull().NotEmpty();
            //RuleFor(x => x.CashbackEarned).NotNull().NotEmpty();
        }
    }
}
