using LoyaltyConsole.Core.Enums;

namespace LoyaltyConsole.Business.DTOs.TransactionDtos
{
    public record TransactionGetDto(int Id, string AppUserId, decimal AmountSpent, BusinessTypes Business,
                                    decimal CashbackEarned, DateTime CreatedDate);
}
