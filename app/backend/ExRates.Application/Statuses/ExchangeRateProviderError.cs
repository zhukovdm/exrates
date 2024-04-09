using FuncSharp;

namespace ExRates.Application;

public sealed class ExchangeRateProviderError
    : Coproduct2<ExchangeRateProviderCommunicationError, ExchangeRateProviderSerializationError>
{
    public ExchangeRateProviderError(ExchangeRateProviderCommunicationError firstValue)
        : base(firstValue) { }

    public ExchangeRateProviderError(ExchangeRateProviderSerializationError secondValue)
        : base(secondValue) { }
}

public sealed class ExchangeRateProviderCommunicationError { }

public sealed class ExchangeRateProviderSerializationError { }
