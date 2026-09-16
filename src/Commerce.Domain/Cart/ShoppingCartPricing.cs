using Commerce.Domain.Common;

namespace Commerce.Domain.Cart;

public readonly record struct ShoppingCartPricing(
    Money Subtotal)
{}
