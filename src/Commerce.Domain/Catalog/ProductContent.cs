namespace Commerce.Domain.Catalog;

public sealed record ProductContent
{
    public ProductContent(string title, string description)
    {
        if(string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("A product title is required.", nameof(title));
        }

        ArgumentNullException.ThrowIfNull(description);

        Title = title.Trim();
        Description = description;
    }

    public string Title { get; }
    public string Description { get; }
}
