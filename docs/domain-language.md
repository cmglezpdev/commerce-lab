# Domain Language — Commerce

This context describes the catalog offers that the shop can configure and sell. It distinguishes a catalog entry from the concrete unit the customer can buy.

## Language

**Product**:
A catalog entry presenting an offer with identity, title, and description. It does not imply that the entry can be purchased directly.
_Avoid_: sellable article, variant

**Simple product**:
A product that represents a single purchasable configuration and does not belong to a variable product.
_Avoid_: simple variant

**Variable product**:
A product that organizes valid options and variants. Its generic entry is not purchased directly.
_Avoid_: purchasable product, variant

**Product option**:
A dimension the buyer can choose, such as `Processor` or `RAM`.
_Avoid_: variant, attribute

**Option value**:
A value allowed for an option, such as `M5 Pro` or `24 GB`.
_Avoid_: option, variant

**Product variant**:
An explicit, purchasable combination that selects exactly one value from each option of a variable product.
_Avoid_: simple product, option

**Sellable item**:
A concrete commercial unit with identity and price that can become a cart line.
_Avoid_: product

**Bundle component**:
An atomic sellable item — a simple product or a product variant — that can be part of a bundle.
_Avoid_: bundle, variable product

**Bundle**:
A purchasable offer with its own price made up of at least two components. A bundle cannot contain another bundle.
_Avoid_: variable product, nested pack

**Shopping cart**:
A mutable selection of items the buyer is considering purchasing. It keeps references by identity and quantities; it does not fix prices or guarantee stock.
_Avoid_: order, stock reservation, checkout

**Cart line**:
A reference to a single sellable item in the cart along with its positive quantity. Two units of the same item form one line with quantity two, not two lines.
_Avoid_: product, snapshot, stock unit

**Checkout**:
The phase after the cart in which purchase conditions are validated again and price and availability can be fixed for a limited period.
_Avoid_: shopping cart, completed order

**Cart pricer**:
A stateless domain service that computes the subtotal of a shopping cart by crossing the identity of its cart lines with the prices of a price book. It operates on objects already in memory; it never searches for or accesses data.
_Avoid_: calculator, PriceResolver, pricing service

**Price book**:
A map of prices per sellable item that the caller resolves and delivers in full to the cart pricer. It may cover more items than those in the cart. It is not the catalog nor a collection of products.
_Avoid_: sellables, catalog, product list

**Cart pricing result**:
The explicit result of pricing the cart: either a subtotal (priced) or the complete list of sellable items without a price (failed). Never an exception carrying data, nor an error flag.
_Avoid_: pricing exception, partial result
