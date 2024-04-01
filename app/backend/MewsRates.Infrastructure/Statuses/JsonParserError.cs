using FuncSharp;

namespace MewsRates.Infrastructure;

public class JsonParserError : Coproduct1<JsonParserFailedParsingError>
{
    public JsonParserError(JsonParserFailedParsingError firstValue)
        : base(firstValue) { }
}

public class JsonParserFailedParsingError
{
    public string Message { get; }

    public JsonParserFailedParsingError(string message) { Message = message; }
}
