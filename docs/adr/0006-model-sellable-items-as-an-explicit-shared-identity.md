# 0006 — Model sellable items as an explicit shared identity

* Status: Accepted
* Date: 2026-09-14

## Context and problem statement

The shop sells three different kinds of purchasable offers: simple products, resolved product variants, and bundles. A shopping cart line, a promotion target, and a bundle component all need to refer to "the thing being bought" without knowing which kind it is. If every component invents its own reference scheme, the model fragments. How should "the purchasable unit" be identified and shared?

## Decision drivers

* The cart, promotions, and bundles must reference purchasable units uniformly.
* A bundle's components must be atomic sellable items — never another bundle — and the model must make that state representable and enforceable.
* One variable product may contain many variants; each is a distinct purchasable unit with its own identity.

## Considered options

* **Polymorphic references everywhere** — the cart would hold `SimpleProduct`/`ProductVariant`/`Bundle` references and branch on type.
* **A typed shared identity (`SellableItemId`)** — one identity concept used uniformly by carts, promotions, and bundles.
* **String or GUID keys without a domain type** — no type safety; IDs of different aggregates become interchangeable.

## Decision outcome

Every purchasable unit — simple product, resolved variant, or bundle — is identified by a shared `SellableItemId`. Cart lines, promotion targets, and bundle components reference this identity. Typed identifiers (even though all wrap a `Guid`) are not interchangeable with each other. Bundles compose only atomic sellable items; bundles cannot nest bundles, and quantities of components must be positive.

### Consequences

* Good: the cart remains decoupled from the catalog — it stores identities, not objects; the application layer resolves current data.
* Good: rules about purchasable units (bundle composition, one line per item) can be stated in terms of a single concept.
* Bad: referential integrity (does this `SellableItemId` still exist and is it available?) crosses aggregates and is deferred to a future query port or application layer (see ADR 0001).
