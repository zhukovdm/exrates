using System.Collections.Generic;
using Newtonsoft.Json;

namespace ExRates.Infrastructure.Cnb;

internal sealed class ExRateDailyResponse
{
    [JsonProperty("rates", Required = Required.Always)]
    public List<ExRateDailyRest>? Rates { get; set; }
}
