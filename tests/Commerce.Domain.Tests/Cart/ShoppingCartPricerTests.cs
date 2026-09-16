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
    public void Price_ShouldThrowError_WhenThereIsMismatchedSellable()
    {
        var cart = new ShoppingCart();
        cart.AddQuantity(SellableItemId.New(), new Quantity(1));

        var priceBook = new Dictionary<SellableItemId, Money>
        {
            [SellableItemId.New()] = new Money(100, Currency.Usd)
        };

        var exception = Assert.Throws<InvalidOperationException>(() => ShoppingCartPricer.Price(cart, priceBook));
        Assert.Equal($"Sellable item with ID {cart.GetItems.First().SellableItemId} not found in price book.", exception.Message);
    }

    [Fact]
    public void Price_ShouldReturnTotal_WhenCartIsPricedCorrectly()
    {
        var cart = new ShoppingCart();
        var sellableItemId = SellableItemId.New();
        cart.AddQuantity(sellableItemId, new Quantity(1));

        var priceBook = new Dictionary<SellableItemId, Money>
        {
            [sellableItemId] = new Money(100, Currency.Usd)
        };

        var result = ShoppingCartPricer.Price(cart, priceBook);

        var money = new Money(100, Currency.Usd);
        Assert.Equal(new ShoppingCartPricing(money), result);
    }
}
    