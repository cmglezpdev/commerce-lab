namespace Commerce.Domain.Catalog;

public sealed record ProductId
{
    public Guid Value { get; }

    private ProductId(Guid value)
    {
        Value = value;
    }

    public static ProductId New() => new(Guid.NewGuid());
}

public sealed record SellableItemId
{
    public Guid Value { get; }

    private SellableItemId(Guid value)
    {
        Value = value;
    }

    public static SellableItemId New() => new(Guid.NewGuid());
}

public sealed record ProductOptionId
{
    public Guid Value { get; }

    private ProductOptionId(Guid value)
    {
        Value = value;
    }

    public static ProductOptionId New() => new(Guid.NewGuid());
}

public sealed record OptionValueId
{
    public Guid Value { get; }

    private OptionValueId(Guid value)
    {
        Value = value;
    }

    public static OptionValueId New() => new(Guid.NewGuid());
}

public sealed record ProductVariantId
{
    public Guid Value { get; }

    private ProductVariantId(Guid value)
    {
        Value = value;
    }

    public static ProductVariantId New() => new(Guid.NewGuid());
}
