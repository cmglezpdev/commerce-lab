using Commerce.Domain.Catalog;
using Commerce.Domain.Common;

namespace Commerce.Domain.Tests.Catalog;

public class ProductTests
{
    [Fact]
    public void Changing_product_content_to_null_throws()
    {
        Product product = new SimpleProduct(
            ProductId.New(),
            SellableItemId.New(),
            new ProductContent("Hello", "World"),
            new Sku("SKU123"),
            new Money(99, Currency.Usd)
        );

        Assert.NotNull(product.Content);
        Assert.Throws<ArgumentNullException>(() => product.ChangeContent(null!));
    }

    [Fact]
    public void New_product_has_no_category()
    {
        Product product = new SimpleProduct(
            ProductId.New(),
            SellableItemId.New(),
            new ProductContent("Hello", "World"),
            new Sku("SKU123"),
            new Money(99, Currency.Usd)
        );

        Assert.Null(product.CategoryId);
    }

    [Fact]
    public void Product_can_be_assigned_to_a_category()
    {
        Product product = new SimpleProduct(
            ProductId.New(),
            SellableItemId.New(),
            new ProductContent("Hello", "World"),
            new Sku("SKU123"),
            new Money(99, Currency.Usd)
        );

        var categoryId = CategoryId.New();
        product.AssignToCategory(categoryId);
        Assert.Equal(categoryId, product.CategoryId);
    }

    [Fact]
    public void Product_can_be_removed_from_its_category()
    {
        Product product = new SimpleProduct(
            ProductId.New(),
            SellableItemId.New(),
            new ProductContent("Hello", "World"),
            new Sku("SKU123"),
            new Money(99, Currency.Usd)
        );

        var categoryId = CategoryId.New();
        product.AssignToCategory(categoryId);
        Assert.Equal(categoryId, product.CategoryId);
        
        product.RemoveFromCategory();
        Assert.Null(product.CategoryId);
    }

    [Fact]
    public void Product_can_be_reclassified()
    {
        Product product = new SimpleProduct(
            ProductId.New(),
            SellableItemId.New(),
            new ProductContent("Hello", "World"),
            new Sku("SKU123"),
            new Money(99, Currency.Usd)
        );

        var categoryId = CategoryId.New();
        product.AssignToCategory(categoryId);
        Assert.Equal(categoryId, product.CategoryId);
        
        var newCategoryId = CategoryId.New();
        product.AssignToCategory(newCategoryId);
        Assert.Equal(newCategoryId, product.CategoryId);
    }
}
