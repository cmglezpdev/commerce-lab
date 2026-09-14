using Commerce.Domain.Common;

namespace Commerce.Domain.Tests;

public class MoneyTests
{
    [Fact]
    public void Formats_amount_with_currency_symbol_and_code()
    {
        var price = new Money(1299.5m, Currency.Usd);
        Assert.Equal("$1299.50 USD", price.ToString());
    }

    [Fact]
    public void Cannot_add_different_currencies()
    {
        var dollars = new Money(1299.5m, Currency.Usd);
        var euros = new Money(30.0m, Currency.Eur);
    
        Assert.Throws<InvalidOperationException>(() => dollars + euros);
    }
}
