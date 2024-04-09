using FuncSharp;

namespace App.Infrastructure;

public sealed class JsonParserError : Coproduct1<JsonParserFailedParsingError>
{
    public JsonParserError(JsonParserFailedParsingError firstValue)
        : base(firstValue) { }
}

public sealed class JsonParserFailedParsingError
{
    public string Message { get; }

    public JsonParserFailedParsingError(string message) { Message = message; }
}
