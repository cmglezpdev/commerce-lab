using Commerce.Domain.Common;

namespace Commerce.Domain.Catalog;

public sealed class SimpleProduct : Product, IBundleComponent
{
    public SimpleProduct(
        ProductId id,
        SellableItemId sellableItemId,
        ProductContent content,
        Sku sku,
        Money price) : base(id, content)
    {
        if(price.Amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(price));
        }

        SellableItemId = sellableItemId;
        Price = price; 
        Sku = sku;
    }

    public SellableItemId SellableItemId { get; }
    public Money Price { get; }
    public Sku Sku { get; }
}
