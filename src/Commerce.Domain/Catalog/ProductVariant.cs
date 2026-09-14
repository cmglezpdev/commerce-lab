using Commerce.Domain.Common;

namespace Commerce.Domain.Catalog;

public sealed record SelectedOption(
    ProductOptionId OptionId,
    OptionValueId ValueId
);

public sealed class ProductVariant : IBundleComponent
{
    private readonly List<SelectedOption> _selection;

    internal ProductVariant(
        ProductVariantId id,
        SellableItemId sellableItemId,
        Sku sku,
        Money price,
        IEnumerable<SelectedOption> selection,
        string? titleOverride,
        string? descriptionOverride)
    {
        if (price.Amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");
        }

        Id = id;
        SellableItemId = sellableItemId;
        Sku = sku;
        Price = price;
        _selection = [.. selection];
        TitleOverride = titleOverride?.Trim();
        DescriptionOverride = descriptionOverride?.Trim();
    }

    public ProductVariantId Id { get; }
    public SellableItemId SellableItemId { get; }
    public Sku Sku { get; }
    public Money Price { get; }
    public IReadOnlyList<SelectedOption> Selection => _selection.AsReadOnly();
    public string? TitleOverride { get; }
    public string? DescriptionOverride { get; }

    public ProductContent ResolveContent(ProductContent parentContent)
    {
        return new ProductContent(
            TitleOverride ?? parentContent.Title,
            DescriptionOverride ?? parentContent.Description
        );
    }
}
