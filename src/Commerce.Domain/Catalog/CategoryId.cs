namespace Commerce.Domain.Catalog;

public sealed record CategoryId
{
    public Guid Value { get; }

    private CategoryId(Guid value)
    {
        Value = value;
    }

    public static CategoryId New() => new (Guid.NewGuid());
}
