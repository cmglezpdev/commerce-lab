using Commerce.Domain.Catalog;
using Commerce.Domain.Common;

namespace Commerce.Domain.Tests.Catalog;

public class VariableProductTests
{
    [Fact]
    public void Variant_selects_one_value_from_every_option()
    {
        var product = new VariableProduct(
            ProductId.New(),
            new ProductContent(
                "Macbook Pro",
                "The latest Macbook Pro model."
            )
        );

        var processor = product.AddOption(
            "Processor",
            "Available chips."
        );

        var ram = product.AddOption(
            "RAM",
            "Unified Memory"
        );

        var m5Pro = product.AddOptionValue(
            processor.Id,
            "M5 Pro",
            "The M5 Pro chip."
        );

        var memory24Gb = product.AddOptionValue(
            ram.Id,
            "24 GB",
            "The 24 GB memory option."
        );

        var variant = product.AddVariant(
            new Sku("MBP-M5PRO-24"),
            new Money(2499m, Currency.Usd),
            [
                new SelectedOption(processor.Id, m5Pro.Id),
                new SelectedOption(ram.Id, memory24Gb.Id)
            ],
            titleOverride: "Macbook Pro with M5 Pro and 24 GB RAM"
        );

        Assert.Equal(2, variant.Selection.Count);
        Assert.Equal(
            "The latest Macbook Pro model.",
            variant.ResolveContent(product.Content).Description);

        Assert.Single(product.Variants);
    }

    [Fact]
    public void Variant_with_invalid_selection_throws_exception()
    {
        var product = new VariableProduct(
            ProductId.New(),
            new ProductContent(
                "Macbook Pro",
                "The latest Macbook Pro model."
            )
        );

        var processor = product.AddOption(
            "Processor",
            "Available chips."
        );

        var ram = product.AddOption(
            "RAM",
            "Unified Memory"
        );

        var m5Pro = product.AddOptionValue(
            processor.Id,
            "M5 Pro",
            "The M5 Pro chip."
        );

        var memory24Gb = product.AddOptionValue(
            ram.Id,
            "24 GB",
            "The 24 GB memory option."
        );

        var variant = product.AddVariant(
            new Sku("MBP-M5PRO-24"),
            new Money(2499m, Currency.Usd),
            [
                new SelectedOption(processor.Id, m5Pro.Id),
                new SelectedOption(ram.Id, memory24Gb.Id)
            ],
            titleOverride: "Macbook Pro with M5 Pro and 24 GB RAM"
        );

        Assert.Throws<InvalidOperationException>(() =>
        {
            var variant = product.AddVariant(
                new Sku("MBP-M5PRO-24"),
                new Money(2499m, Currency.Usd),
                [
                    new SelectedOption(processor.Id, m5Pro.Id),
                    new SelectedOption(ram.Id, memory24Gb.Id)
                ],
                titleOverride: "Macbook Pro with M5 Pro and 24 GB RAM"
            );
        });
    }

}
