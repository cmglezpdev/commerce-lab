namespace Commerce.Domain.Catalog;

public sealed class ProductOption
{
    private readonly List<OptionValue> _values = [];

    internal ProductOption(
        ProductOptionId id,
        string name,
        string description
    )
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("An option name must be provided.", nameof(name));
        }

        ArgumentNullException.ThrowIfNull(description);

        Id = id;
        Name = name.Trim();
        Description = description.Trim();
    }

    public ProductOptionId Id { get; }
    public string Name { get; }
    public string Description { get; }
    public IReadOnlyList<OptionValue> Values => _values;

    internal OptionValue AddValue(string name, string description)
    {
        if(_values.Any(v => v.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new ArgumentException("An option value with the same name already exists.", nameof(name));
        }

        var value = new OptionValue(
            OptionValueId.New(), 
            Id, 
            name, 
            description);

        _values.Add(value);
        return value;
    }
}



public sealed class OptionValue
{
    internal OptionValue(
        OptionValueId id,
        ProductOptionId optionId,
        string name,
        string description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "An option value name is required.",
                nameof(name));
        }

        ArgumentNullException.ThrowIfNull(description);

        Id = id;
        OptionId = optionId;
        Name = name.Trim();
        Description = description.Trim();
    }

    public OptionValueId Id { get; }
    public ProductOptionId OptionId { get; }
    public string Name { get; }
    public string Description { get; }
}