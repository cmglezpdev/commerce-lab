using Commerce.Domain.Catalog;
using Commerce.Domain.Common;

namespace Commerce.Domain.Cart;

public sealed class ShoppingCart
{
    private List<ShoppingCartLine> Items { get; set; } = [];
    
    public ShoppingCart(){}

    public void AddQuantity(SellableItemId itemId, Quantity quantity)
    {
        var existingItem = Items.FirstOrDefault(item => item.SellableItemId == itemId);
        if(existingItem != null)
        {
            existingItem.IncreaseQuantity(quantity);
        }
        else
        {
            Items.Add(new ShoppingCartLine(itemId, quantity));
        }
    }

    public void ReduceQuantity(SellableItemId itemId, Quantity quantity)
    {
        var existingItem =
            Items.FirstOrDefault(item => item.SellableItemId == itemId)
            ?? throw new ArgumentException("Item not found in the cart.", nameof(itemId));

        var remaining = existingItem.Quantity.Value - quantity.Value;

        if(remaining < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Cannot reduce quantity below zero.");
        }

        if(remaining > 0)
        {
            existingItem.DecreaseQuantity(quantity);
        } else
        {
            Items = [.. Items.Where(item => item.SellableItemId != itemId)];
        }
    }

    public int Count => Items.Count;
    public int TotalQuantity => Items.Sum(item => item.Quantity.Value);

    public IReadOnlyList<ShoppingCartLine> GetItems => [.. Items];
}


public sealed class ShoppingCartLine(
    SellableItemId sellableItemId,
    Quantity quantity
)
{
    public SellableItemId SellableItemId { get; } = sellableItemId;
    public Quantity Quantity { get; private set; } = quantity;

    public void IncreaseQuantity(Quantity quantity)
    {
        Quantity = new Quantity(Quantity.Value + quantity.Value);
    }

    public void DecreaseQuantity(Quantity quantity)
    {
        Quantity = new Quantity(Quantity.Value - quantity.Value);
    }
}
