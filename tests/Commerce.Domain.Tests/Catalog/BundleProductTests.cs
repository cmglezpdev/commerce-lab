using Commerce.Domain.Catalog;
using Commerce.Domain.Common;

namespace Commerce.Domain.Tests.Catalog;

public class BundleProductTests
{
    [Fact]
    public void Bundle_accepts_two_simple_products()
    {
        var mouse = CreateSimple("MOUSE-1", 99m);
        var keyboard = CreateSimple("KEYBOARD-1", 199m);
    
        var bundle = new BundleProduct(
            ProductId.New(),
            SellableItemId.New(),
            new ProductContent("Desk bundle", "Mouse & Keyboard"),
            new Sku("DESK-BUNDLE"),
            new Money(199m, Currency.Usd),
            [
                BundleComponent.From(mouse, new Quantity(1)),
                BundleComponent.From(keyboard, new Quantity(1)),
            ]);

        Assert.Equal(2, bundle.Components.Count);
        // Guarantee that the bundle implements ISellable but not IBundleComponent
        Assert.IsAssignableFrom<ISellable>(bundle);
        Assert.IsNotAssignableFrom<IBundleComponent>(bundle);
    }

    private static SimpleProduct CreateSimple(string sku, decimal price)
    {
        return new SimpleProduct(
            ProductId.New(),
            SellableItemId.New(),
            new ProductContent(sku, string.Empty),
            new Sku(sku),
            new Money(price, Currency.Usd)
        );
    }
}
