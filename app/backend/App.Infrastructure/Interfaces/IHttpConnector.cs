using System;
using System.Threading.Tasks;
using FuncSharp;

namespace App.Infrastructure;

public interface IHttpConnector
{
    /// <summary>
    /// Perform GET request toward input URI.
    /// </summary>
    /// <param name="uri"></param>
    Task<Try<string, HttpConnectorError>> GetAsync(Uri uri);
}
