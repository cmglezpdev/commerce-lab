using Commerce.Domain.Catalog;

namespace Commerce.Domain.Tests.Catalog;

public class CollectionTests
{
    [Fact]
    public void CreateCollection_ShouldTrimNameAndGetIdentityAndStartEmpty()
    {
        var collection = new Collection("  Back to school  ");

        Assert.NotNull(collection.Id);
        Assert.Equal("Back to school", collection.Name);
        Assert.Empty(collection.ProductIds);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void CreateCollection_ShouldRejectInvalidName(string? invalidName)
    {
        Assert.Throws<ArgumentException>(() => new Collection(invalidName!));
    }

    [Fact]
    public void AddProduct_ShouldContainProductId()
    {
        var collection = new Collection("Back to school");
        var productId = ProductId.New();

        collection.AddProduct(productId);

        Assert.Contains(productId, collection.ProductIds);
    }

    [Fact]
    public void AddProduct_ShouldKeepSingleMembershipWhenAddingSameProductTwice()
    {
        var collection = new Collection("Back to school");
        var productId = ProductId.New();

        collection.AddProduct(productId);
        collection.AddProduct(productId);

        Assert.Single(collection.ProductIds);
        Assert.Contains(productId, collection.ProductIds);
    }

    [Fact]
    public void RemoveProduct_ShouldKeepOtherProduct()
    {
        var collection = new Collection("Back to school");
        var firstProductId = ProductId.New();
        var secondProductId = ProductId.New();
        collection.AddProduct(firstProductId);
        collection.AddProduct(secondProductId);

        collection.RemoveProduct(firstProductId);

        Assert.DoesNotContain(firstProductId, collection.ProductIds);
        Assert.Contains(secondProductId, collection.ProductIds);
    }

    [Fact]
    public void AddProduct_ShouldAllowSameProductInTwoCollections()
    {
        var firstCollection = new Collection("Back to school");
        var secondCollection = new Collection("Summer deals");
        var productId = ProductId.New();

        firstCollection.AddProduct(productId);
        secondCollection.AddProduct(productId);

        Assert.Contains(productId, firstCollection.ProductIds);
        Assert.Contains(productId, secondCollection.ProductIds);
    }
}
