using LoyaltyConsole.Core.Enums;

namespace LoyaltyConsole.Business.DTOs.TransactionDtos
{
    public record TransactionGetDto(int Id, int CustomerId, decimal AmountSpent, BusinessTypes Business,
                                    decimal CashbackEarned, DateTime CreatedDate);
}
