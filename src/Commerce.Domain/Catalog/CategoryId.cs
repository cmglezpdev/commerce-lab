namespace Commerce.Domain.Catalog;

public readonly record struct CategoryId(Guid Value)
{
    public static CategoryId New() => new (Guid.NewGuid());
}
