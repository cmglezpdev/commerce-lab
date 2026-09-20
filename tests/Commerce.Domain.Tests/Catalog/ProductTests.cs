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

    #region Category tests

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
    #endregion

    #region Product Tags tests
        [Fact]
    public void New_product_has_no_tags_by_default()
    {
        var product = new SimpleProduct(
            ProductId.New(),
            SellableItemId.New(),
            new ProductContent("Name", "Description"),
            new Sku("SKU-001"),
            new Money(100, Currency.Mxn)
        );

        Assert.Empty(product.Tags);
    }

    [Fact]
    public void Add_tag_to_product() 
    {
        var product = new SimpleProduct(
            ProductId.New(),
            SellableItemId.New(),
            new ProductContent("Name", "Description"),
            new Sku("SKU-001"),
            new Money(100, Currency.Mxn)
        );

        var tag = new ProductTag("Refurbished");
        product.AddTag(tag);

        Assert.Contains(tag, product.Tags);
    }

    [Fact]
    public void Add_same_tag_to_product_only_adds_once()
    {
        var product = new SimpleProduct(
            ProductId.New(),
            SellableItemId.New(),
            new ProductContent("Name", "Description"),
            new Sku("SKU-001"),
            new Money(100, Currency.Mxn)
        );

        var tag = new ProductTag("Refurbished");
        var anotherTag = new ProductTag("  refurbished ");
        product.AddTag(tag);
        product.AddTag(tag);
        product.AddTag(anotherTag);

        Assert.Single(product.Tags);
        Assert.Contains(tag, product.Tags);
    }

    [Fact]
    public void Remove_tag_from_product()
    {
        var product = new SimpleProduct(
            ProductId.New(),
            SellableItemId.New(),
            new ProductContent("Name", "Description"),
            new Sku("SKU-001"),
            new Money(100, Currency.Mxn)
        );

        var tag = new ProductTag("Refurbished");
        var anotherTag = new ProductTag("other-tag");

        product.AddTag(tag);
        product.AddTag(anotherTag);
        Assert.Contains(tag, product.Tags);
        Assert.Contains(anotherTag, product.Tags);
        Assert.Equal(2, product.Tags.Count);

        product.RemoveTag(tag);

        Assert.DoesNotContain(tag, product.Tags);
        Assert.Contains(anotherTag, product.Tags);
        Assert.Single(product.Tags);
    }
    #endregion
}
