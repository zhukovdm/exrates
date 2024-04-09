using System.Collections.Generic;

namespace ExRates.Api;

public sealed class ExRatesOptions
{
    public static readonly string Section = "ExRates";

    public List<string> SourceCurrencies { get; set; } = null!;
}
