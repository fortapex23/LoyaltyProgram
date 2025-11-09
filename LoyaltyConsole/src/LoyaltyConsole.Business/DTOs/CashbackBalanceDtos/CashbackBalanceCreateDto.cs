using FluentValidation;

namespace LoyaltyConsole.Business.DTOs.CashbackBalanceDtos
{
    public record CashbackBalanceCreateDto(int CustomerId, decimal TotalCashback, decimal CashbackRedeemed);

    public class CashbackBalanceCreateDtoValidator : AbstractValidator<CashbackBalanceCreateDto>
    {
        public CashbackBalanceCreateDtoValidator()
        {
            RuleFor(x => x.CustomerId).NotNull().NotEmpty();
            RuleFor(x => x.TotalCashback).NotNull();
            RuleFor(x => x.CashbackRedeemed).NotNull();
        }
    }
}
