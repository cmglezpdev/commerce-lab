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
    public CategoryId? CategoryId { get; private set; }

    public void ChangeContent(ProductContent content)
    {
        ArgumentNullException.ThrowIfNull(content);
        Content = content;
    }

    public void AssignToCategory(CategoryId categoryId)
    {
        CategoryId = categoryId;
    }

    public void RemoveFromCategory()
    {
        CategoryId = null;
    }
}
