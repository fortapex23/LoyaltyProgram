namespace LoyaltyConsole.MVC.Enums
{
    public enum Currencies
    {
        AZN,
        USD,
        EURO
    }

    public static class CurrencyExtensions
    {
        public static string GetSymbol(this Currencies currency)
        {
            return currency switch
            {
                Currencies.AZN => "₼",
                Currencies.USD => "$",
                Currencies.EURO => "€",
                _ => ""
            };
        }
    }
}
