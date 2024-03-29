using FuncSharp;

namespace MewsRates.Domain;

public class ExchangeRate
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

    private static bool IsValidValue(decimal value) => value > 0.0M;

    public static Option<ExchangeRate> Create(Option<Currency> sourceCurrency,
        Option<Currency> targetCurrency, decimal value)
    {
        return from s in sourceCurrency
               from t in targetCurrency
               select IsValidValue(value) ? new ExchangeRate(s, t, value) : null;
    }
}
