using System.Net;
using FuncSharp;

namespace App.Infrastructure;

public sealed class HttpConnectorError
    : Coproduct2<HttpConnectorFailedRequest, HttpConnectorUnexpectedStatusCode>
{
    public HttpConnectorError(HttpConnectorFailedRequest firstValue)
        : base(firstValue) { }

    public HttpConnectorError(HttpConnectorUnexpectedStatusCode secondValue)
        : base(secondValue) { }
}

public sealed class HttpConnectorFailedRequest
{
    public string Message { get; }

    public HttpConnectorFailedRequest(string message) { Message = message; }
}

public sealed class HttpConnectorUnexpectedStatusCode
{
    public HttpStatusCode Code { get; }

    public HttpConnectorUnexpectedStatusCode(HttpStatusCode code) { Code = code; }
}
