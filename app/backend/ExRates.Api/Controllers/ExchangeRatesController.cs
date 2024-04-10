using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using ExRates.Application;
using ExRates.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ExRates.Api.V1;

[ApiController]
[Route("api/v1/exrates")]
public sealed class ExchangeRatesController : ControllerBase
{
    private readonly ILogger<ExchangeRatesController> _logger;
    private readonly ExRatesOptions _options;
    private readonly IExchangeRatesService _service;

    public ExchangeRatesController(ILogger<ExchangeRatesController> logger,
        IOptionsSnapshot<ExRatesOptions> options, IExchangeRatesService service)
    {
        _logger = logger;
        _options = options.Value;
        _service = service;
    }

    [HttpGet, Route("", Name = "GetExchangeRates")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ExchangeRate>>> GetExchangeRates()
    {
        var sourceCurrencies = _options.SourceCurrencies.Select(label => Currency.CreateUnsafe(label));

        return (await _service.GetExchangeRatesAsync(sourceCurrencies))
            .Match<ActionResult<IEnumerable<ExchangeRate>>>(
                rates => Ok(rates.Select(rate => new ExchangeRate()
                {
                    SourceCurrency = rate.SourceCurrency.Code,
                    TargetCurrency = rate.TargetCurrency.Code,
                    Value = rate.Value
                })),
                error => error.Match(_ => new StatusCodeResult(StatusCodes.Status500InternalServerError))
            );
    }
}
