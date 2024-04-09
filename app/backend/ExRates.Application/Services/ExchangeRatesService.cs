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
    private readonly ILogger<ExchangeRatesService> _logger;
    private readonly IExchangeRateProvider _provider;

    public ExchangeRatesService(ILogger<ExchangeRatesService> logger, IExchangeRateProvider provider)
    {
        _logger = logger;
        _provider = provider;
    }

    public async Task<Try<IEnumerable<ExchangeRate>, ExchangeRatesServiceError>> GetExchangeRatesAsync(
        IEnumerable<Currency> sourceCurrencies)
    {
        return (await _provider.GetAvailableExchangeRatesAsync())
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
