namespace Commerce.Domain.Catalog;

public sealed record CollectionId
{
    public Guid Value { get; }

    private CollectionId(Guid value)
    {
        Value = value;
    }

    public static CollectionId New() => new(Guid.NewGuid());
}
