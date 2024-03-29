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

    public static Option<ExchangeRate> Create(Currency sourceCurrency, Currency targetCurrency, decimal value)
    {
        return (value > 0.0M)
            ? Option.Valued<ExchangeRate>(new(sourceCurrency, targetCurrency, value))
            : Option.Empty<ExchangeRate>();
    }
}
