using System.Threading.Tasks;
using ExRates.Infrastructure.Cnb;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExRates.Infrastructure.Tests;

[TestClass]
public sealed class CnbOpenApiExchangeRateProviderTests
{
    private ILogger<CnbOpenApiExchangeRateProvider> l = null!;
    private IOptionsSnapshot<CnbOptions> o = null!;
    private IJsonParser p = null!;

    [TestInitialize]
    public void Initialize()
    {
        l = new Microsoft.Extensions.Logging.Abstractions.NullLogger<CnbOpenApiExchangeRateProvider>();
        o = new CnbOptionsSnapshot();
        p = new JsonParser();
    }

    [TestCleanup]
    public void Cleanup() { }

    [TestMethod]
    public async Task ShouldReturnErrorDueToFailingHttpConnector()
    {
        var prv = new CnbOpenApiExchangeRateProvider(l, o, new FailingHttpConnector(), p);
        var res = await prv.GetAvailableExchangeRatesAsync();
        res.Match(
            suc => Assert.Fail(),
            err => { } // target branch
        );
    }
}
