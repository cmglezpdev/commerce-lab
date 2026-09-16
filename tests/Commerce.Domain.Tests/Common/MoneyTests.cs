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

    [Fact]
    public void MoneyZero_ShouldReturnZeroMoney()
    {
        var zeroUsd = Money.Zero(Currency.Eur);
        Assert.Equal(new Money(0, Currency.Eur), zeroUsd);
    }

    [Fact]
    public void Money_ShouldComputeMultiplyCorrectly()
    {
        var price = new Money(1299.5m, Currency.Usd);
        var quantity = new Quantity(2);
        
        var result = price * quantity;

        Assert.Equal(new Money(2599.0m, Currency.Usd), result);
    }
}
