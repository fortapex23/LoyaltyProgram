namespace LoyaltyConsole.Business.DTOs.CashbackBalanceDtos
{
    public record CashbackBalanceGetDto(int Id,string AppUserId, decimal TotalCashback, decimal CashbackRedeemed,
                                        decimal CashbackAvailable, DateTime CreatedDate);
}
