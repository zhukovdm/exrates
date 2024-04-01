using Newtonsoft.Json;

namespace MewsRates.Infrastructure.Cnb;

internal sealed class ExRateDailyRest
{
    [JsonProperty("amount", Required = Required.Always)]
    public long? Amount { get; set; }

    [JsonProperty("currencyCode", Required = Required.Always)]
    public string? CurrencyCode { get; set; }

    [JsonProperty("rate", Required = Required.Always)]
    public decimal? Rate { get; set; }

    #region Ignored properties

    [JsonIgnore]
    [JsonProperty("country")]
    public string? Country { get; set; }

    [JsonIgnore]
    [JsonProperty("currency")]
    public string? Currency { get; set; }

    [JsonIgnore]
    [JsonProperty("validFor")]
    public string? ValidFor { get; set; }

    [JsonIgnore]
    [JsonProperty("order")]
    public long? Order { get; set; }

    #endregion
}
