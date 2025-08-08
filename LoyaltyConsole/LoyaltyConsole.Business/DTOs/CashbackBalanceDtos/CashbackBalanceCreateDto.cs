using FluentValidation;

namespace LoyaltyConsole.Business.DTOs.CashbackBalanceDtos
{
    public record CashbackBalanceCreateDto(string AppUserId, decimal TotalCashback, decimal CashbackRedeemed);

    public class CashbackBalanceCreateDtoValidator : AbstractValidator<CashbackBalanceCreateDto>
    {
        public CashbackBalanceCreateDtoValidator()
        {
            RuleFor(x => x.AppUserId).NotNull().NotEmpty();
            RuleFor(x => x.TotalCashback).NotNull();
            RuleFor(x => x.CashbackRedeemed).NotNull();
        }
    }
}
