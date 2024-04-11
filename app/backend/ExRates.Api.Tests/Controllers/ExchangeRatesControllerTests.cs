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
    private ILogger<ExchangeRatesController> l = null!;
    private IOptionsSnapshot<ExRatesOptions> o = null!;

    [TestInitialize]
    public void Initialize()
    {
        l = new Microsoft.Extensions.Logging.Abstractions.NullLogger<ExchangeRatesController>();
        o = new ExRatesOptionsSnapshot();
    }

    [TestCleanup]
    public void Cleanup() { }

    [TestMethod]
    public async Task ShouldReturn500DueToFailingService()
    {
        var ctr = new ExchangeRatesController(l, o, new FailingExchangeRatesService());
        var res = (await ctr.GetExchangeRates()).Result as StatusCodeResult;
        Assert.IsTrue(res?.StatusCode == StatusCodes.Status500InternalServerError);
    }
}
