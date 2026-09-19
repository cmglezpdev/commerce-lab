using Commerce.Domain.Catalog;

namespace Commerce.Domain.Tests.Catalog;

public class CategoryTests
{
    [Fact]
    public void Category_ShouldCreateRootCategory()
    {
        var category = Category.CreateRoot("Electronics");
        Assert.Null(category.ParentId);
        Assert.Equal("Electronics", category.Name);
    }

    [Fact]
    public void Category_ShouldCreateChildCategory()
    {
        var rootCategory = Category.CreateRoot("Electronics");
        var childCategory = rootCategory.CreateChild("Laptops");
        Assert.Equal(rootCategory.Id, childCategory.ParentId);
        Assert.Equal("Laptops", childCategory.Name);
    }

    [Fact]
    public void Category_ShouldCreateThreeLevelCategory()
    {
        var rootCategory = Category.CreateRoot("Electronics");
        var childCategory = rootCategory.CreateChild("Laptops");
        var grandChildCategory = childCategory.CreateChild("Gaming Laptops");
        Assert.Equal(rootCategory.Id, childCategory.ParentId);
        Assert.Equal(childCategory.Id, grandChildCategory.ParentId);
        Assert.Equal("Electronics", rootCategory.Name);
        Assert.Equal("Laptops", childCategory.Name);
        Assert.Equal("Gaming Laptops", grandChildCategory.Name);
    }

    [Fact]
    public void Category_ShouldTrimName()
    {
        var category = Category.CreateRoot("  Electronics  ");
        Assert.Equal("Electronics", category.Name);
    }

    [Fact]
    public void Category_ShouldThrowExceptionForInvalidName()
    {
        Assert.Throws<ArgumentException>(() => Category.CreateRoot("  "));
    }
}
