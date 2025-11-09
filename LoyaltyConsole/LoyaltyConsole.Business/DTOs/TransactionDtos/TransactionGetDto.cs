using LoyaltyConsole.Core.Enums;

namespace LoyaltyConsole.Business.DTOs.TransactionDtos
{
    public record TransactionGetDto(int Id, int CustomerId, decimal AmountSpent, BusinessTypes Business, Currency Currency,
                                    decimal CashbackEarned, DateTime TransactionDate, DateTime CreatedDate);
}
