using Commerce.Domain.Catalog;
using Commerce.Domain.Common;

namespace Commerce.Domain.Cart;

public abstract record CartPricingResult
{
    public sealed record Priced(Money Subtotal) : CartPricingResult;

    public sealed record Failed(IReadOnlyCollection<SellableItemId> Missing) : CartPricingResult;
}
