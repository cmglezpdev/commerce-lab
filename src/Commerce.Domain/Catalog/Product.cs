namespace Commerce.Domain.Catalog;

public abstract class Product
{
    protected Product(ProductId id, ProductContent content)
    {
        ArgumentNullException.ThrowIfNull(content);

        Id = id;
        Content = content; 
    }
    
    public ProductId Id { get; }
    public ProductContent Content { get; private set; }

    public void ChangeContent(ProductContent content)
    {
        ArgumentNullException.ThrowIfNull(content);
        Content = content;
    }
}
