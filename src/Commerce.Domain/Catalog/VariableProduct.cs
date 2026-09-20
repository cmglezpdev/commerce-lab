using Commerce.Domain.Common;

namespace Commerce.Domain.Catalog;

public sealed class VariableProduct : Product
{
    private readonly List<ProductOption> _options = [];
    private readonly List<ProductVariant> _variants = [];

    public VariableProduct(ProductId id, ProductContent content) : base(id, content) {}

    public IReadOnlyList<ProductOption> Options => _options.AsReadOnly();
    public IReadOnlyList<ProductVariant> Variants => _variants.AsReadOnly();

    public ProductOption AddOption(string name, string description)
    {
        if(_variants.Count > 0)
        {
            throw new InvalidOperationException("Cannot add options once variants have been created.");
        }

        if(_options.Any(o => string.Equals(name, o.Name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException($"The option '{name}' already exists.");
        }

        var option = new ProductOption(
            ProductOptionId.New(),
            name,
            description);

        _options.Add(option);
        return option;
    }

    public OptionValue AddOptionValue(
        ProductOptionId optionId,
        string name,
        string description)
    {
        var option = FindOption(optionId);
        return option.AddValue(name, description);
    }

    public ProductVariant AddVariant(
    Sku sku,
    Money price,
    IReadOnlyCollection<SelectedOption> selection,
    string? titleOverride = null,
    string? descriptionOverride = null)
    {
        ArgumentNullException.ThrowIfNull(sku);
        ArgumentNullException.ThrowIfNull(price);
        ArgumentNullException.ThrowIfNull(selection);
        ValidateSelection(selection);

        if(_variants.Any(variant =>
        HasSameSelection(variant.Selection, selection)))
        {
            throw new InvalidOperationException(
                "A variant with the same selection already exists.");
        }

        var variant = new ProductVariant(
            ProductVariantId.New(),
            SellableItemId.New(),
            sku,
            price,
            selection,
            titleOverride,
            descriptionOverride);

        _variants.Add(variant);
        return variant;
    }

    private ProductOption FindOption(ProductOptionId optionId)
    {
        return _options.SingleOrDefault(option => option.Id == optionId)
            ?? throw new InvalidOperationException(
                "The option does not belong to this product.");
    }

    private void ValidateSelection(IReadOnlyCollection<SelectedOption> selection)
    {
        // Ensure that there is at least one option before validating the selection.
        if(_options.Count == 0)
        {
            throw new InvalidOperationException(
                "Add at least one option before adding variants.");
        }

        // Ensure that exactly one value is selected for each option.
        var selectedOptionCount = selection
            .Select(item => item.OptionId)
            .Distinct()
            .Count();

        if (selectedOptionCount != _options.Count)
        {
            throw new InvalidOperationException(
                "Select exactly one value from every option.");
        }

        // Ensure that each selected value belongs to its corresponding option.
        foreach (var selected in selection)
        {
            var option = FindOption(selected.OptionId);

            if (option.Values.All(value => value.Id != selected.ValueId))
            {
                throw new InvalidOperationException(
                    "The value does not belong to the selected option.");
            }
        }
    }

    private static bool HasSameSelection(
        IReadOnlyCollection<SelectedOption> existing,
        IReadOnlyCollection<SelectedOption> candidate)
    {
        return existing.Count == candidate.Count &&
            candidate.All(existing.Contains);
    }
}
