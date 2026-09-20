namespace Commerce.Domain.Catalog;

public abstract class Product
{
    private readonly HashSet<ProductTag> _tags = [];

    protected Product(ProductId id, ProductContent content)
    {
        ArgumentNullException.ThrowIfNull(content);

        Id = id;
        Content = content; 
    }
    
    public ProductId Id { get; }
    public ProductContent Content { get; private set; }
    public CategoryId? CategoryId { get; private set; }
    public IReadOnlySet<ProductTag> Tags => _tags;

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

    public void AddTag(ProductTag tag)
    {
        _tags.Add(tag);
    }

    public void RemoveTag(ProductTag tag)
    {
        _tags.Remove(tag);
    }
}
