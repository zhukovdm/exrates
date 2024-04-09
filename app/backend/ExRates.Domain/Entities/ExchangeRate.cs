using FuncSharp;

namespace ExRates.Domain;

public sealed class ExchangeRate
{
    private ExchangeRate(Currency sourceCurrency, Currency targetCurrency, decimal value)
    {
        SourceCurrency = sourceCurrency;
        TargetCurrency = targetCurrency;
        Value = value;
    }

    public Currency SourceCurrency { get; }

    public Currency TargetCurrency { get; }

    public decimal Value { get; }

    /// <summary></summary>
    /// <param name="sourceCurrency">Currency converted from</param>
    /// <param name="targetCurrency">Currency converted to</param>
    /// <param name="value">Amount of target currency for a unit of source currency</param>
    public static Option<ExchangeRate> Create(Option<Currency> sourceCurrency,
        Option<Currency> targetCurrency, decimal value)
    {
        return from s in sourceCurrency
               from t in targetCurrency
               select new ExchangeRate(s, t, value);
    }
}
