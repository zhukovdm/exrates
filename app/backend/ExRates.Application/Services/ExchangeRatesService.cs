using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using ExRates.Domain;
using FuncSharp;
using Microsoft.Extensions.Logging;

namespace ExRates.Application;

public sealed class ExchangeRatesService : IExchangeRatesService
{
    private readonly ILogger<ExchangeRatesService> logger;
    private readonly IExchangeRateProvider provider;

    public ExchangeRatesService(ILogger<ExchangeRatesService> logger, IExchangeRateProvider provider)
    {
        this.logger = logger;
        this.provider = provider;
    }

    public async Task<Try<IEnumerable<ExchangeRate>, ExchangeRatesServiceError>> GetExchangeRatesAsync(
        IEnumerable<Currency> sourceCurrencies)
    {
        return (await provider.GetAvailableExchangeRatesAsync())
            .Map<IEnumerable<ExchangeRate>, ExchangeRatesServiceError>(
                rates =>
                {
                    var currencies = sourceCurrencies.Aggregate(
                        ImmutableHashSet<Currency>.Empty, (acc, item) => acc.Add(item));

                    return from rate in rates where currencies.Contains(rate.SourceCurrency) select rate;
                },
                error =>
                {
                    // error.Match();
                    return new ExchangeRatesServiceError(new ExchangeRatesServiceInternalError());
                }
            );
    }
}
