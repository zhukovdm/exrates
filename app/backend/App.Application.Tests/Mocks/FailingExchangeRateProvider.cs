using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain;
using FuncSharp;

namespace App.Application.Tests;

public sealed class FailingExchangeRateProvider : IExchangeRateProvider
{
    public Task<Try<IEnumerable<ExchangeRate>, ExchangeRateProviderError>> GetAvailableExchangeRatesAsync()
    {
        return Task.FromResult(Try.Error<IEnumerable<ExchangeRate>, ExchangeRateProviderError>(
            new ExchangeRateProviderError(new ExchangeRateProviderCommunicationError())));
    }
}
