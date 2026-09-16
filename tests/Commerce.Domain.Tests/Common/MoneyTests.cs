using Commerce.Domain.Common;

namespace Commerce.Domain.Tests.Common;

public class MoneyTests
{
    [Fact]
    public void Money_ShouldFormatCorrectly()
    {
        var price = new Money(1299.5m, Currency.Usd);
        Assert.Equal("$1299.50 USD", price.ToString());
    }

    [Fact]
    public void AddDifferentCurrencies_ShouldThrowError()
    {
        var dollars = new Money(1299.5m, Currency.Usd);
        var euros = new Money(30.0m, Currency.Eur);
    
        Assert.Throws<InvalidOperationException>(() => dollars + euros);
    }
}
