using FluentValidation;

namespace LoyaltyConsole.Business.DTOs.CashbackBalanceDtos
{
    public record CashbackBalanceUpdateDto(int CustomerId, decimal TotalCashback, decimal CashbackRedeemed,
                                        decimal CashbackAvailable);

    public class CashbackBalanceUpdateDtoValidator : AbstractValidator<CashbackBalanceUpdateDto>
    {
        public CashbackBalanceUpdateDtoValidator()
        {
            RuleFor(x => x.CustomerId).NotNull().NotEmpty();
            RuleFor(x => x.TotalCashback).NotNull();
            RuleFor(x => x.CashbackRedeemed).NotNull();
            RuleFor(x => x.CashbackAvailable).NotNull();
        }
    }
}
