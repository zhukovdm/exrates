using System;
using System.Text.RegularExpressions;
using FuncSharp;

namespace MewsRates.Domain;

public sealed class Currency
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

    public bool Equals(Currency? obj) => obj is not null && Code == obj.Code;

    public override bool Equals(object? obj) => Equals(obj as Currency);

    public override int GetHashCode() => Code.GetHashCode();

    /// <summary></summary>
    /// <param name="code">Three-letter ISO 4217 code of the currency.</param>
    public static Option<Currency> Create(string? code)
    {
        return code is not null && re.Value.IsMatch(code)
            ? Option.Valued<Currency>(new(code.ToUpper())) : Option.Empty<Currency>();
    }

    /// <summary></summary>
    /// <param name="code">Three-letter ISO 4217 code of the currency.</param>
    public static Currency CreateUnsafe(string code) => new(code);
}
