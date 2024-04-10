using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExRates.Application;
using ExRates.Domain;
using FuncSharp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ExRates.Infrastructure.Cnb;

public sealed class CnbOpenApiExchangeRateProvider : IExchangeRateProvider
{
    /// <summary>
    /// Hardcoded target currency specific for CNB Open API endpoint.
    /// </summary>
    private static readonly Lazy<Option<Currency>> targetCurrency = new(() => Currency.Create("CZK"));

    private readonly ILogger<CnbOpenApiExchangeRateProvider> _logger;
    private readonly CnbOptions _options;
    private readonly IHttpConnector _connector;
    private readonly IJsonParser _parser;

    public CnbOpenApiExchangeRateProvider(ILogger<CnbOpenApiExchangeRateProvider> logger,
        IOptionsSnapshot<CnbOptions> options, IHttpConnector connector, IJsonParser parser)
    {
        _logger = logger;
        _options = options.Value;
        _connector = connector;
        _parser = parser;
    }

    public async Task<Try<IEnumerable<ExchangeRate>, ExchangeRateProviderError>> GetAvailableExchangeRatesAsync()
    {
        var targetUrl = new Uri($"{_options.OpenApi.BaseUrl}?date={DateTime.Now.ToString("yyyy-MM-dd")}&lang=EN");

        return (await _connector.GetAsync(targetUrl))
            .MapError(error =>
            {
                error.Match(
                    e => _logger.LogError("Failed GET request towards {TargetUrl}: {Message}", targetUrl, e.Message),
                    e => _logger.LogError("Unable to GET data from {TargetUrl} due to unexpected status code {Code}.", targetUrl, e.Code)
                );
                return new ExchangeRateProviderError(new ExchangeRateProviderCommunicationError());
            })
            .FlatMap(json => _parser.Parse<ExRateDailyResponse>(json)
                .MapError(error =>
                {
                    error.Match(
                        e => _logger.LogError("JSON parser failed: {Message}", e.Message)
                    );
                    return new ExchangeRateProviderError(new ExchangeRateProviderSerializationError());
                }))
            .Map(resp => from item in Transform(resp) where item.NonEmpty select item.Get());
    }

    private IEnumerable<Option<ExchangeRate>> Transform(ExRateDailyResponse response)
    {
        return from item in response.Rates
               where item.Amount != 0 // !
               select ExchangeRate.Create(Currency.Create(item.CurrencyCode!), targetCurrency.Value, item.Rate!.Value / (decimal)item.Amount!.Value);
    }
}
