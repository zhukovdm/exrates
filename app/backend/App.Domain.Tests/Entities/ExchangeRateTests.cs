using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Domain.Tests;

[TestClass]
public class ExchangeRateTests
{
    [TestMethod]
    public void ShouldCreateValuedOption()
    {
        // Arrange
        var czk = Currency.Create("CZK");
        var rate = ExchangeRate.Create(czk, czk, 1);

        // Act
        var res = rate.NonEmpty;

        // Assert
        Assert.IsTrue(res);
    }
}
