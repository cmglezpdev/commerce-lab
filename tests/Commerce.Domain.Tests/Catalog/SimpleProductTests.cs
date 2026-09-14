using Commerce.Domain.Catalog;
using Commerce.Domain.Common;

namespace Commerce.Domain.Tests.Catalog;

public class SimpleProductTests
{
    [Fact]
    public void Simple_product_is_sellable_and_can_enter_a_bundle()
    {
        var product = new SimpleProduct(
            ProductId.New(),
            SellableItemId.New(),
            new ProductContent("Mouse", "Wireless mouse"),
            new Sku("mouse-1"),
            new Money(99m, Currency.Usd));

        Assert.IsAssignableFrom<ISellable>(product);
        Assert.IsAssignableFrom<IBundleComponent>(product);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(0)]
    [InlineData(-1)]
    public void Simple_product_cannot_have_negative_price(decimal price)
    {
        if(price >= 0)
        {
            var product = new SimpleProduct(
                ProductId.New(),
                SellableItemId.New(),
                new ProductContent("Mouse", "Wireless mouse"),
                new Sku("mouse-1"),
                new Money(price, Currency.Usd));
            return;
        }

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var product = new SimpleProduct(
                ProductId.New(),
                SellableItemId.New(),
                new ProductContent("Mouse", "Wireless mouse"),
                new Sku("mouse-1"),
                new Money(price, Currency.Usd));
        });
    }
}
