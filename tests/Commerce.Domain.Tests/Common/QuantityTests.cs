using Commerce.Domain.Common;

namespace Commerce.Domain.Tests.Common;

public class QuantityTests
{
    [Fact]
    public void Quantity_ShouldCreateCorrectly()
    {
        var quantity = new Quantity(5);
        Assert.Equal(5, quantity.Value);
    }

    [Fact]
    public void Quantity_ShouldThrowError_WhenNegativeNumber()
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new Quantity(-1));
        Assert.Contains("Quantity must be greater than zero.", exception.Message);
    }

    [Fact]
    public void Quantity_ShouldThrowError_WhenZero()
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new Quantity(0));
        Assert.Contains("Quantity must be greater than zero.", exception.Message);
    }
}
