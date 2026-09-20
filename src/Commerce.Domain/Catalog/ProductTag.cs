namespace Commerce.Domain.Catalog;

public sealed record ProductTag
{
    public string Value { get; }

    public ProductTag(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        Value = name.Trim().ToLowerInvariant();
    }
}
