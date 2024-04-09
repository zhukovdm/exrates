using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application;
using App.Domain;
using FuncSharp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace App.Infrastructure.Cnb;

public sealed class CnbOpenApiExchangeRateProvider : IExchangeRateProvider
{
    /// <summary>
    /// Hardcoded target currency specific for CNB Open API endpoint.
    /// </summary>
    private static readonly Lazy<Option<Currency>> targetCurrency = new(() => Currency.Create("CZK"));

    /// <summary>
    /// Target URL for CNB Open API endpoint with a prefix configurable from app settings.
    /// </summary>
    private Uri GetTargetUri()
    {
        return new($"{_options.Value.OpenApi.BaseUrl}?date={DateTime.Now.ToString("yyyy-MM-dd")}&lang=EN");
    }

    private readonly ILogger<CnbOpenApiExchangeRateProvider> _logger;
    private readonly IOptionsSnapshot<CnbOptions> _options;
    private readonly IHttpConnector _connector;
    private readonly IJsonParser _parser;
    private readonly Uri TargetUrl;

    public CnbOpenApiExchangeRateProvider(ILogger<CnbOpenApiExchangeRateProvider> logger,
        IOptionsSnapshot<CnbOptions> options, IHttpConnector connector, IJsonParser parser)
    {
        _logger = logger;
        _options = options;
        _connector = connector;
        _parser = parser;
        TargetUrl = GetTargetUri();
    }

    public async Task<Try<IEnumerable<ExchangeRate>, ExchangeRateProviderError>> GetAvailableExchangeRatesAsync()
    {
        return (await _connector.GetAsync(TargetUrl))
            .MapError(error =>
            {
                error.Match(
                    e => _logger.LogError("Failed GET request toward {TargetUrl}: {Message}", TargetUrl, e.Message),
                    e => _logger.LogError("Unable to GET data from {TargetUrl} due to unexpected status code {Code}.", TargetUrl, e.Code)
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
