using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FuncSharp;

namespace App.Infrastructure;

public sealed class HttpConnector : IHttpConnector
{
    private readonly HttpClient _httpClient;

    public HttpConnector(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Try<string, HttpConnectorError>> GetAsync(Uri url)
    {
        return await (await Try.CatchAsync<HttpResponseMessage, Exception>(_ => _httpClient.GetAsync(url)))
            .MapError(e => new HttpConnectorError(new HttpConnectorFailedRequest(e.Message)))
            .FlatMapAsync(async m =>
            {
                return m.StatusCode == HttpStatusCode.OK
                    ? Try.Success<string, HttpConnectorError>(await m.Content.ReadAsStringAsync())
                    : Try.Error<string, HttpConnectorError>(new(new HttpConnectorUnexpectedStatusCode(m.StatusCode)));
            });
    }
}
