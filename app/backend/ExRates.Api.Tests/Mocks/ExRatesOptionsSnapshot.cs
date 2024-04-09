using Microsoft.Extensions.Options;

namespace ExRates.Api.Tests;

public class ExRatesOptionsSnapshot : IOptionsSnapshot<ExRatesOptions>
{
    public ExRatesOptions Value
    {
        get => new()
        {
            SourceCurrencies = new() { "CZK", "EUR", "USD" }
        };
    }

    public ExRatesOptions Get(string? name)
    {
        throw new System.NotImplementedException();
    }
}
