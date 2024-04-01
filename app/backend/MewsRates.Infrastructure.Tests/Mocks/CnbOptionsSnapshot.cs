using MewsRates.Infrastructure.Cnb;
using Microsoft.Extensions.Options;

namespace MewsRates.Infrastructure.Tests;

public class CnbOptionsSnapshot : IOptionsSnapshot<CnbOptions>
{
    public CnbOptions Value
    {
        get => new()
        {
            OpenApi = new() { BaseUrl = "https://www.example.com" }
        };
    }

    public CnbOptions Get(string? name)
    {
        throw new System.NotImplementedException();
    }
}
