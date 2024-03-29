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

    public override int GetHashCode() => Code.GetHashCode();

    /// <param name="code">Three-letter ISO 4217 code of the currency.</param>
    public static Option<Currency> Create(string code)
    {
        var predicate = code is not null && re.Value.IsMatch(code);
        return Option.Create<Currency>(predicate ? new(code.ToUpper()) : null);
    }
}
