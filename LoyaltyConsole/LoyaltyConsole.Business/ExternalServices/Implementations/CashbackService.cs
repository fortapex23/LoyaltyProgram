using LoyaltyConsole.Business.ExternalServices.Interfaces;
using LoyaltyConsole.Core.Enums;

namespace LoyaltyConsole.Business.ExternalServices.Implementations
{
    public class CashbackService : ICashbackService
    {
        public decimal GetCashbackRate(BusinessTypes businessType)
        {
            return businessType switch
            {
                BusinessTypes.Supermarket => 0.04m,
                BusinessTypes.Restaurant => 0.02m,
                BusinessTypes.Cafe => 0.03m,
                BusinessTypes.ClothingStore => 0.02m,
                BusinessTypes.ElectronicsStore => 0.02m,
                BusinessTypes.Pharmacy => 0.03m,
                BusinessTypes.GasStation => 0.03m,
                BusinessTypes.OnlineShopping => 0.05m,
                BusinessTypes.Airline => 0.01m,
                BusinessTypes.Hotel => 0.05m,
                BusinessTypes.Cinema => 0.05m,
                BusinessTypes.Bookstore => 0.03m,
                BusinessTypes.FitnessCenter => 0.04m,
                BusinessTypes.BeautySalon => 0.01m,
                BusinessTypes.TaxiService => 0.02m,
                BusinessTypes.FurnitureStore => 0.03m,
                BusinessTypes.JewelryStore => 0.04m,
                BusinessTypes.Bakery => 0.05m,
                BusinessTypes.FastFood => 0.04m,
                
            };
        }

        public decimal CalculateCashback(decimal amountSpent, BusinessTypes businessType)
        {
            var rate = GetCashbackRate(businessType);
            return Math.Round(amountSpent * rate, 2, MidpointRounding.AwayFromZero);
        }
    }
}
