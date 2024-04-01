using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MewsRates.Domain.Tests;

[TestClass]
public class CurrencyTests
{
    [TestMethod]
    public void ShouldCreateValuedOption()
    {
        // Arrange
        var czk = Currency.Create("CZK");

        // Act
        var res = czk.NonEmpty;

        // Assert
        Assert.IsTrue(res);
    }
}
