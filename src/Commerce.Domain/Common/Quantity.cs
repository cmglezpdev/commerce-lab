namespace Commerce.Domain.Common;

public readonly record struct Quantity
{
    public Quantity(int value)
    {
        if(value <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Quantity must be greater than zero.");
        }

        Value = value;
    }

    public int Value { get; }
}
