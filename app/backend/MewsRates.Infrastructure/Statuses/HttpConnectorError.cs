using System.Net;
using FuncSharp;

namespace MewsRates.Infrastructure;

public class HttpConnectorError
    : Coproduct2<HttpConnectorFailedRequest, HttpConnectorUnexpectedStatusCode>
{
    public HttpConnectorError(HttpConnectorFailedRequest firstValue)
        : base(firstValue) { }

    public HttpConnectorError(HttpConnectorUnexpectedStatusCode secondValue)
        : base(secondValue) { }
}

public class HttpConnectorFailedRequest
{
    public string Message { get; }

    public HttpConnectorFailedRequest(string message) { Message = message; }
}

public class HttpConnectorUnexpectedStatusCode
{
    public HttpStatusCode Code { get; }

    public HttpConnectorUnexpectedStatusCode(HttpStatusCode code) { Code = code; }
}
