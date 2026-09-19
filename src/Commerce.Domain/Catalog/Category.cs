namespace Commerce.Domain.Catalog;

public sealed class Category
{
    public CategoryId Id { get; }
    public string Name { get; }
    public CategoryId? ParentId { get; }

    private Category(string name, CategoryId? parentId = null)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Category name cannot be null or whitespace.", nameof(name));
        }

        Id = CategoryId.New();
        Name = name.Trim();
        ParentId = parentId;
    }

    public static Category CreateRoot(string name)
    {
        return new Category(name);
    }

    public Category CreateChild(string name)
    {
        return new Category(name, this.Id);   
    }
}
