namespace Commerce.Domain.Catalog;

public sealed class Collection
{
    private readonly HashSet<ProductId> productIds = [];

    public CollectionId Id { get; }
    public string Name {get; }
    public IReadOnlySet<ProductId> ProductIds => productIds;

    public Collection(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Collection name cannot be null or whitespace.", nameof(name));
        }

        Id = CollectionId.New();
        Name = name.Trim();
    }

    public void AddProduct(ProductId productId)
    {
        ArgumentNullException.ThrowIfNull(productId);
        productIds.Add(productId);
    }

    public void RemoveProduct(ProductId productId)
    {
        ArgumentNullException.ThrowIfNull(productId);
        productIds.Remove(productId);
    }
}
