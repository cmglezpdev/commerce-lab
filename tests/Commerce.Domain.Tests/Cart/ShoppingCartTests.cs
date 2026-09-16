using Commerce.Domain.Catalog;
using Commerce.Domain.Common;
using Commerce.Domain.Cart;

namespace Commerce.Domain.Tests.Cart;

public class ShoppingCartTests
{
    
    [Fact]
    public void AddQuantity_ShouldIncreaseItemQuantity()
    {
        var cart = new ShoppingCart();
        cart.AddQuantity(SellableItemId.New(), new Quantity(2));

        Assert.Equal(2, cart.TotalQuantity);
        Assert.Single(cart.GetItems);
        Assert.Equal(1, cart.Count);
    }

    [Fact]
    public void ReduceQuantity_ShouldDecreaseItemQuantity()
    {
        var cart = new ShoppingCart();
        var itemId = SellableItemId.New();
        cart.AddQuantity(itemId, new Quantity(2));
        cart.ReduceQuantity(itemId, new Quantity(1));

        Assert.Equal(1, cart.TotalQuantity);
        Assert.Single(cart.GetItems);
        Assert.Equal(1, cart.Count);
    }

    [Fact]
    public void ReduceQuantity_ShouldRemoveItemWhenQuantityBecomesZero()
    {

        var cart = new ShoppingCart();

        var itemId = SellableItemId.New();
        cart.AddQuantity(itemId, new Quantity(2));
        cart.ReduceQuantity(itemId, new Quantity(2));

        Assert.Equal(0, cart.TotalQuantity);
        Assert.Empty(cart.GetItems);
        Assert.Equal(0, cart.Count);
    }

    [Fact]
    public void ReduceQuantity_ShouldThrowError_WhenReducingMoreThanExistingQuantity()
    {
        var cart = new ShoppingCart();
        var itemId = SellableItemId.New();
        cart.AddQuantity(itemId, new Quantity(2));

        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => 
            cart.ReduceQuantity(itemId, new Quantity(3)));
        Assert.Contains("Quantity to reduce exceeds the current quantity.", exception.Message);
    }

    [Fact]
    public void AddSameItem_ShouldIncreaseQuantity()
    {
        var itemId = SellableItemId.New();
        var cart = new ShoppingCart();
        cart.AddQuantity(itemId, new Quantity(2));
        cart.AddQuantity(itemId, new Quantity(3));

        Assert.Equal(5, cart.TotalQuantity);
        Assert.Single(cart.GetItems);
        Assert.Equal(1, cart.Count);
    }

    [Fact]
    public void ReduceQuantityToNonexistentItem_ShouldThrowsError()
    {
        var itemId = SellableItemId.New();

        var cart = new ShoppingCart();
        var exception = Assert.Throws<ArgumentException>(() => 
            cart.ReduceQuantity(itemId, new Quantity(1)));
        Assert.Equal("Item not found in the cart. (Parameter 'itemId')", exception.Message);
    }
}
