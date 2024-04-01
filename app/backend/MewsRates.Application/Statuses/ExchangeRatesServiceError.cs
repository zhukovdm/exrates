using FuncSharp;

namespace MewsRates.Application;

public sealed class ExchangeRatesServiceInternalError { }

public sealed class ExchangeRatesServiceError : Coproduct1<ExchangeRatesServiceInternalError>
{
    public ExchangeRatesServiceError(ExchangeRatesServiceInternalError firstValue)
        : base(firstValue) { }
}
