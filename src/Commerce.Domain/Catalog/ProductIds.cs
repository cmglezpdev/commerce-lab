namespace Commerce.Domain.Catalog;

public readonly record struct ProductId(Guid Value)
{
    public static ProductId New() => new(Guid.NewGuid());
}

public readonly record struct SellableItemId(Guid Value)
{
    public static SellableItemId New() => new(Guid.NewGuid());
}

public readonly record struct ProductOptionId(Guid Value)
{
    public static ProductOptionId New() => new(Guid.NewGuid());
}

public readonly record struct OptionValueId(Guid Value)
{
    public static OptionValueId New() => new(Guid.NewGuid());
}

public readonly record struct ProductVariantId(Guid Value)
{
    public static ProductVariantId New() => new(Guid.NewGuid());
}