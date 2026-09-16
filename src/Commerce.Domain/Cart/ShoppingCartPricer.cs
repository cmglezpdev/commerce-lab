using Commerce.Domain.Catalog;
using Commerce.Domain.Common;

namespace Commerce.Domain.Cart;

public static class ShoppingCartPricer
{
    public static ShoppingCartPricing? Price(ShoppingCart cart, IReadOnlyDictionary<SellableItemId, Money> priceBook)
    {
        if(cart.Count == 0) return null;

        var subtotal = Money.Zero(priceBook.First().Value.Currency);
        
        foreach(var line in cart.GetItems)
        {
            if(!priceBook.ContainsKey(line.SellableItemId))
            {
                throw new InvalidOperationException($"Sellable item with ID {line.SellableItemId} not found in price book.");
            }

            var sellablePrice = priceBook[line.SellableItemId];

            if(sellablePrice.Currency != subtotal.Currency)
            {
                throw new InvalidOperationException($"Currency mismatch for sellable item with ID {line.SellableItemId}.");
            }

            subtotal += sellablePrice * line.Quantity;
        }

        return new ShoppingCartPricing(subtotal);
    }
}