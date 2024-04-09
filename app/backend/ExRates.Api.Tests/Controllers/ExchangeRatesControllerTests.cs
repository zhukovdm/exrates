using System.Threading.Tasks;
using ExRates.Api.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExRates.Api.Tests;

[TestClass]
public sealed class ExchangeRatesControllerTests
{
    private ILogger<ExchangeRatesController> _l = null!;
    private IOptionsSnapshot<ExRatesOptions> _o = null!;

    [TestInitialize]
    public void Initialize()
    {
        _l = new Microsoft.Extensions.Logging.Abstractions.NullLogger<ExchangeRatesController>();
        _o = new ExRatesOptionsSnapshot();
    }

    [TestCleanup]
    public void Cleanup() { }

    [TestMethod]
    public async Task ShouldReturn500DueToFailingService()
    {
        var ctr = new ExchangeRatesController(_l, _o, new FailingExchangeRatesService());
        var res = (await ctr.GetExchangeRates()).Result as StatusCodeResult;
        Assert.IsTrue(res?.StatusCode == StatusCodes.Status500InternalServerError);
    }
}
