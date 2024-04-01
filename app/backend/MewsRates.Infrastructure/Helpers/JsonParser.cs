using System;
using FuncSharp;
using Newtonsoft.Json;

namespace MewsRates.Infrastructure;

public sealed class JsonParser : IJsonParser
{
    public Try<T, JsonParserError> Parse<T>(string json)
    {
        return Try.Catch<Try<T, JsonParserError>, Exception>(
            _ => Try.Success<T, JsonParserError>(JsonConvert.DeserializeObject<T>(json)!),
            e => Try.Error<T, JsonParserError>(new(new JsonParserFailedParsingError(e.Message)))
        );
    }
}
