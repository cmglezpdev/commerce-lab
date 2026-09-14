namespace Commerce.Domain.Catalog;

public readonly record struct Sku
{
    public Sku(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("A SKU value is required.", nameof(value));
        }

        Value = value.Trim().ToUpperInvariant();
    }
    public string Value { get; }

    public override string ToString() => Value;
}
