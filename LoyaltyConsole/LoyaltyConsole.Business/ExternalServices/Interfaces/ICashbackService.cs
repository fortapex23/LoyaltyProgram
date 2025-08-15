using LoyaltyConsole.Core.Enums;

namespace LoyaltyConsole.Business.ExternalServices.Interfaces
{
    public interface ICashbackService
    {
        decimal GetCashbackRate(BusinessTypes businessType);
        decimal CalculateCashback(decimal amountSpent, BusinessTypes businessType);
    }
}
