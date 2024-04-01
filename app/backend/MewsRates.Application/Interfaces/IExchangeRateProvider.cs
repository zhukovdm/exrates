using System.Collections.Generic;
using System.Threading.Tasks;
using FuncSharp;
using MewsRates.Domain;

namespace MewsRates.Application;

public interface IExchangeRateProvider
{
    /// <summary>
    /// Get all available exchange rates specific for a given provider.
    /// </summary>
    public Task<Try<IEnumerable<ExchangeRate>, ExchangeRateProviderError>> GetAvailableExchangeRatesAsync();
}
