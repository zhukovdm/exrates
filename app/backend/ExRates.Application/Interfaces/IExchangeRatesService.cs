using System.Collections.Generic;
using System.Threading.Tasks;
using ExRates.Domain;
using FuncSharp;

namespace ExRates.Application;

public interface IExchangeRatesService
{
    /// <summary>
    /// Return exchange rates among the specified currencies that are defined
    /// by the source. But only those defined by the source, do not return
    /// calculated exchange rates. E.g. if the source contains "CZK/USD" but not
    /// "USD/CZK", do not return exchange rate "USD/CZK" with value calculated
    /// as 1 / "CZK/USD". If the source does not provide some of the currencies,
    /// ignore them.
    /// <br/>
    /// We assume that there is only <b>one</b> target currency, determined by
    /// a provider. For example, CNB provides pairs [source]/CZK, so that we get
    /// amount of Czech kronas for a _unit_ of the source currency.
    /// </summary>
    Task<Try<IEnumerable<ExchangeRate>, ExchangeRatesServiceError>> GetExchangeRatesAsync(
        IEnumerable<Currency> sourceCurrencies);
}
