using FuncSharp;

namespace MewsRates.Infrastructure;

public interface IJsonParser
{
    /// <summary>
    /// Parses string into a POCO of a given type or fails otherwise.
    /// </summary>
    /// <param name="json">Entity serialization</param>
    Try<T, JsonParserError> Parse<T>(string json);
}
