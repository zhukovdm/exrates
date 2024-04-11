using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FuncSharp;

namespace ExRates.Infrastructure;

public sealed class HttpConnector : IHttpConnector
{
    private readonly HttpClient httpClient;

    public HttpConnector(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Try<string, HttpConnectorError>> GetAsync(Uri url)
    {
        return await (await Try.CatchAsync<HttpResponseMessage, Exception>(_ => httpClient.GetAsync(url)))
            .MapError(e => new HttpConnectorError(new HttpConnectorFailedRequest(e.Message)))
            .FlatMapAsync(async m =>
            {
                return m.StatusCode == HttpStatusCode.OK
                    ? Try.Success<string, HttpConnectorError>(await m.Content.ReadAsStringAsync())
                    : Try.Error<string, HttpConnectorError>(new(new HttpConnectorUnexpectedStatusCode(m.StatusCode)));
            });
    }
}
