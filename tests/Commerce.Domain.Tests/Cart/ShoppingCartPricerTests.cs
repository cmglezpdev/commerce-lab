using Commerce.Domain.Cart;
using Commerce.Domain.Catalog;
using Commerce.Domain.Common;

namespace Commerce.Domain.Tests.Cart;

public class ShoppingCartPricerTests
{
    [Fact]
    public void Price_ShouldReturnNull_WhenCartIsEmpty()
    {
        var cart = new ShoppingCart();
        var result = ShoppingCartPricer.Price(
            cart,
            new Dictionary<SellableItemId, Money>());
    
        Assert.Null(result);
    }

    [Fact]
    public void Price_ShouldReturnFailedResult_WhenThereIsMismatchedSellable()
    {
        var cart = new ShoppingCart();
        cart.AddQuantity(SellableItemId.New(), new Quantity(1));

        var priceBook = new Dictionary<SellableItemId, Money>
        {
            [SellableItemId.New()] = new Money(100, Currency.Usd)
        };

        var cartResult = ShoppingCartPricer.Price(cart, priceBook);
        var failed = Assert.IsType<CartPricingResult.Failed>(cartResult);
        Assert.Equal([cart.GetItems[0].SellableItemId], failed.Missing);
    }

    [Fact]
    public void Price_ShouldReturnTotal_WhenCartIsPricedCorrectly()
    {
        var cart = new ShoppingCart();
        var productASellableItemId = SellableItemId.New();
        cart.AddQuantity(productASellableItemId, new Quantity(2));
        var productBSellableItemId = SellableItemId.New();
        cart.AddQuantity(productBSellableItemId, new Quantity(3));

        var priceBook = new Dictionary<SellableItemId, Money>
        {
            [productASellableItemId] = new Money(10, Currency.Usd),
            [productBSellableItemId] = new Money(50, Currency.Usd)
        };

        var result = ShoppingCartPricer.Price(cart, priceBook);

        var money = new Money(2 * 10 + 3 * 50, Currency.Usd);
        var pricer = Assert.IsType<CartPricingResult.Priced>(result);
        Assert.Equal(money, pricer.Subtotal);
    }

    [Fact]
    public void Price_ShouldThrowError_WhenCurrencyMismatch()
    {
        var cart = new ShoppingCart();
        var productASellableItemId = SellableItemId.New();
        cart.AddQuantity(productASellableItemId, new Quantity(1));
        var productBSellableItemId = SellableItemId.New();
        cart.AddQuantity(productBSellableItemId, new Quantity(1));

        var priceBook = new Dictionary<SellableItemId, Money>
        {
            [productASellableItemId] = new Money(100, Currency.Eur),
            [productBSellableItemId] = new Money(100, Currency.Usd)
        };

        var exception = Assert.Throws<InvalidOperationException>(() => ShoppingCartPricer.Price(cart, priceBook));
        Assert.Contains($"Currency mismatch", exception.Message);
    
    }
}