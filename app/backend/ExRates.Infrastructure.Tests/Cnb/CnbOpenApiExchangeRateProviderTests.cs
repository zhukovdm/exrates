using System.Threading.Tasks;
using ExRates.Infrastructure.Cnb;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExRates.Infrastructure.Tests;

[TestClass]
public sealed class CnbOpenApiExchangeRateProviderTests
{
    private ILogger<CnbOpenApiExchangeRateProvider> _l = null!;
    private IOptionsSnapshot<CnbOptions> _o = null!;
    private IJsonParser _p = null!;

    [TestInitialize]
    public void Initialize()
    {
        _l = new Microsoft.Extensions.Logging.Abstractions.NullLogger<CnbOpenApiExchangeRateProvider>();
        _o = new CnbOptionsSnapshot();
        _p = new JsonParser();
    }

    [TestCleanup]
    public void Cleanup() { }

    [TestMethod]
    public async Task ShouldReturnErrorDueToFailingHttpConnector()
    {
        var prv = new CnbOpenApiExchangeRateProvider(_l, _o, new FailingHttpConnector(), _p);
        var res = await prv.GetAvailableExchangeRatesAsync();
        res.Match(
            suc => Assert.Fail(),
            err => { } // target branch
        );
    }
}
