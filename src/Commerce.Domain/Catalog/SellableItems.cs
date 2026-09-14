using Commerce.Domain.Common;

namespace Commerce.Domain.Catalog;

public interface ISellable
{
    SellableItemId SellableItemId { get; }
    Money Price { get; }
}

public interface IBundleComponent : ISellable;
