using Commerce.Domain.Common;

namespace Commerce.Domain.Catalog;

public sealed record BundleComponent
{
    private BundleComponent(
        SellableItemId sellableItemId,
        Quantity quantity
    )
    {
        ItemId = sellableItemId;
        Quantity = quantity;
    }

    public SellableItemId ItemId { get; }
    public Quantity Quantity { get; }

    public static BundleComponent From(
        IBundleComponent item,
        Quantity quantity
    )
    {
        return new BundleComponent(item.SellableItemId, quantity);
    }
}

public sealed class BundleProduct : Product, ISellable
{
    private readonly List<BundleComponent> _components = [];

    public BundleProduct(
        ProductId id, 
        SellableItemId sellableItemId,
        ProductContent content,
        Sku sku,
        Money price,
        IEnumerable<BundleComponent> components) : base(id, content)
    {
        ArgumentNullException.ThrowIfNull(components, nameof(components));
        if(price.Amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(price), "Price must be greater than or equal to zero.");
        }

        _components = [.. components];
    
        if(_components.Count < 2)
        {
            throw new InvalidOperationException("A bundle must contain at least two components.");
        }

        var distinctItemCount = _components
            .Select(c => c.ItemId)
            .Distinct()
            .Count();
        

        if(distinctItemCount != _components.Count)
        {
            throw new InvalidOperationException("A bundle cannot contain duplicate components.");
        }

        SellableItemId = sellableItemId;
        Sku = sku;
        Price = price;
    }

    public SellableItemId SellableItemId { get; }
    public Sku Sku { get; }
    public Money Price { get; }
    public IReadOnlyList<BundleComponent> Components => _components.AsReadOnly();
}
