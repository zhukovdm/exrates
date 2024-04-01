using FuncSharp;

namespace MewsRates.Application;

public sealed class ExchangeRatesServiceError : Coproduct1<ExchangeRatesServiceInternalError>
{
    public ExchangeRatesServiceError(ExchangeRatesServiceInternalError firstValue)
        : base(firstValue) { }
}

public sealed class ExchangeRatesServiceInternalError { }
