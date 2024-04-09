using System.ComponentModel.DataAnnotations;

namespace ExRates.Api;

public sealed class ExchangeRate
{
    /// <example>DKK</example>
    [Required]
    [RegularExpression("^[A-Z]{3}$")]
    public string SourceCurrency { get; init; } = null!;

    /// <example>CZK</example>
    [Required]
    [RegularExpression("^[A-Z]{3}$")]
    public string TargetCurrency { get; init; } = null!;

    /// <example>3.393</example>
    [Required]
    public decimal Value { get; init; }
}
