using Commerce.Domain.Catalog;

namespace Commerce.Domain.Tests.Catalog;

public class ProductTagTests
{
    [Fact]
    public void ProductTag_ShouldNormalize_Name()
    {
        var tag = new ProductTag("  refurbished ");
        var tag2 = new ProductTag("REFURBISHED");

        Assert.Equal(tag.Value, tag2.Value);
        Assert.Equal("refurbished", tag.Value);
        Assert.Equal(tag, tag2);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void ProductTag_ShouldThrow_OnNullOrEmptyName(string? value)
    {
        if (value is null)
            Assert.Throws<ArgumentNullException>(() => new ProductTag(value!));
        else
            Assert.Throws<ArgumentException>(() => new ProductTag(value!));
    }
}
