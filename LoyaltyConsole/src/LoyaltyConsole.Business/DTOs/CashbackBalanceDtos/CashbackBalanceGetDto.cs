namespace LoyaltyConsole.Business.DTOs.CashbackBalanceDtos
{
    public record CashbackBalanceGetDto(int Id,int CustomerId, decimal TotalCashback, decimal CashbackRedeemed,
                                        decimal CashbackAvailable, DateTime CreatedDate);
}
