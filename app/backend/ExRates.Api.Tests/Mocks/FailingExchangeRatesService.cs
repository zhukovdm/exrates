using System.Collections.Generic;
using System.Threading.Tasks;
using ExRates.Application;
using ExRates.Domain;
using FuncSharp;

namespace ExRates.Api.Tests;

internal sealed class FailingExchangeRatesService : IExchangeRatesService
{
    public Task<Try<IEnumerable<Domain.ExchangeRate>, ExchangeRatesServiceError>> GetExchangeRatesAsync(IEnumerable<Currency> sourceCurrencies)
    {
        return Task.FromResult(Try.Error<IEnumerable<Domain.ExchangeRate>, ExchangeRatesServiceError>(
            new ExchangeRatesServiceError(new ExchangeRatesServiceInternalError())));
    }
}
