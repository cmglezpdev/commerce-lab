using System.Globalization;

namespace Commerce.Domain.Common;

public sealed record Money(decimal Amount, Currency Currency)
{
    public static Money operator +(Money left, Money right)
    {
        if(left.Currency != right.Currency)
        {
            throw new InvalidOperationException("Cannot add amounts with different currencies.");
        }

        return new Money(left.Amount + right.Amount, left.Currency);
    }
    public static Money Zero(Currency currency)
    {
        return new Money(0, currency);
    }

    public static Money operator *(Money left, Quantity quantity)
    {
        return new Money(left.Amount * quantity.Value, left.Currency);
    }
    public override string ToString()
    {
        var symbol = Currency switch
        {
            Currency.Usd => "$",
            Currency.Mxn => "MX$",
            Currency.Eur => "€",
            _ => throw new ArgumentOutOfRangeException("Unsupported currency.")
        };

        var amount = Amount.ToString(
            "0.00",
            CultureInfo.InvariantCulture);

        return $"{symbol}{amount} {Currency.ToString().ToUpperInvariant()}";
    }
}