using System.Linq;
using System.Threading.Tasks;
using ExRates.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExRates.Application.Tests;

[TestClass]
public class ExchangeRatesServiceTests
{
    private ILogger<ExchangeRatesService> _l = null!;

    [TestInitialize]
    public void Initialize()
    {
        _l = new Microsoft.Extensions.Logging.Abstractions.NullLogger<ExchangeRatesService>();
    }

    [TestCleanup]
    public void Cleanup() { }

    [TestMethod]
    public async Task ShouldResolveIntoServiceErrorDueToFailingExchangeProvider()
    {
        var srv = new ExchangeRatesService(_l, new FailingExchangeRateProvider());
        var res = await srv.GetExchangeRatesAsync(Enumerable.Empty<Currency>());
        res.Match(
            suc => Assert.Fail(),
            err => { } // target branch
        );
    }
}
