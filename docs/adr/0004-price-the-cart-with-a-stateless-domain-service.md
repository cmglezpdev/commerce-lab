# 0004 — Price the cart with a stateless domain service

* Status: Accepted
* Date: 2026-09-18

## Context and problem statement

The shopping cart intentionally stores neither prices nor catalog content: it keeps references (`SellableItemId`) and quantities only. Yet clients need to know the subtotal. Computing it requires information the `ShoppingCart` aggregate does not and should not hold — current prices for every line — which spans multiple aggregates and would couple the cart to the catalog. Where does cart pricing live?

## Decision drivers

* The cart must not hold live catalog objects; the application layer resolves current data when it matters.
* Pricing crosses the `ShoppingCart`, catalog data, and (later) promotions — no single aggregate can own it.
* A single price book map is cheaper and simpler than lookups per line, and keeps the domain pure (no repositories, no queries).

## Considered options

* **The cart resolves prices itself** — couples the cart to the catalog and breaks its no-content rule.
* **A dedicated `ShoppingCartPricer` domain service** — stateless, pure, fed by the caller.
* **An application-service pricing engine with caching and async lookups** — infrastructure concerns, premature in a pure domain.

## Decision outcome

A stateless `ShoppingCartPricer` domain service computes the subtotal. The caller resolves current prices into a **price book** — a map of prices per sellable item that may cover more items than the cart contains — and passes it in full to the pricer. The pricer operates only on objects already in memory and never searches for data.

### Consequences

* Good: the cart stays free of catalog knowledge; the pricer is trivially testable and deterministic.
* Good: the contract makes the cost of data access explicit — the caller owns it.
* Bad: the caller must assemble a complete price book before pricing; a partial map is an explicit failure (see ADR 0005).
