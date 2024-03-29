using System;
using System.Text.RegularExpressions;
using FuncSharp;

namespace MewsRates.Domain;

public class Currency
{
    private static readonly Lazy<Regex> re = new(() => new(@"^[A-Za-z]{3}$", RegexOptions.Compiled));

    /// <summary>
    /// Three-letter ISO 4217 code of the currency.
    /// </summary>
    public string Code { get; }

    private Currency(string code)
    {
        Code = code;
    }

    /// <param name="code">Three-letter ISO 4217 code of the currency.</param>
    public static Option<Currency> Create(string code)
    {
        return re.Value.IsMatch(code)
            ? Option.Valued<Currency>(new(code.ToUpper()))
            : Option.Empty<Currency>();
    }
}
