using System;
using System.Threading.Tasks;
using FuncSharp;

namespace ExRates.Infrastructure.Tests;

public sealed class FailingHttpConnector : IHttpConnector
{
    public Task<Try<string, HttpConnectorError>> GetAsync(Uri uri)
    {
        return Task.FromResult(Try.Error<string, HttpConnectorError>(
            new HttpConnectorError(new HttpConnectorFailedRequest(string.Empty))));
    }
}
