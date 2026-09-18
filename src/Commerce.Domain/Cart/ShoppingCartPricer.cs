using Commerce.Domain.Catalog;
using Commerce.Domain.Common;

namespace Commerce.Domain.Cart;

public static class ShoppingCartPricer
{
    public static CartPricingResult? Price(ShoppingCart cart, IReadOnlyDictionary<SellableItemId, Money> priceBook)
    {
        if(cart.Count == 0) return null;
        if(priceBook.Count == 0)
        {
            throw new InvalidOperationException("Price book cannot be empty.");
        }

        var subtotal = Money.Zero(priceBook.First().Value.Currency);
        var missingItems = new List<SellableItemId>();
        
        foreach(var line in cart.GetItems)
        {
            if(!priceBook.ContainsKey(line.SellableItemId))
            {
                missingItems.Add(line.SellableItemId);
                continue;
            }

            var sellablePrice = priceBook[line.SellableItemId];

            if(sellablePrice.Currency != subtotal.Currency)
            {
                throw new InvalidOperationException($"Currency mismatch for sellable item with ID {line.SellableItemId}.");
            }

            subtotal += sellablePrice * line.Quantity;
        }

        return missingItems.Count > 0 
            ? new CartPricingResult.Failed(missingItems) 
            : new CartPricingResult.Priced(subtotal);
    }
}