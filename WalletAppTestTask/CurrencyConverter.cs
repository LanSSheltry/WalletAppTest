using WalletAppTestTask.DbContext;

namespace WalletAppTestTask
{

    //Currency converter with actual data on 03.12.2023
    public static class CurrencyConverter
    {
        public static decimal ConvertMoney(Currency from, Currency to, decimal amount)
        {
            Dictionary<Currency, decimal> coefs = new Dictionary<Currency, decimal>
            {
                {Currency.USD, 1 },
                {Currency.EUR, (decimal)0.91855},
                {Currency.UAH, (decimal)36.477674},
                {Currency.CAD, (decimal)1.34995}
            };

            var converted = amount / coefs[from] * coefs[to];

            return converted;
        }
    }
}
