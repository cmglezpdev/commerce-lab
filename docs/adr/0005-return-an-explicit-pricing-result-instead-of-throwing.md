# 0005 — Return an explicit pricing result instead of throwing

* Status: Accepted
* Date: 2026-09-18

## Context and problem statement

Pricing the cart can fail in an expected, business-meaningful way: a sellable item has no price in the price book. This is not an exceptional fault — it is a normal outcome that the caller must handle. How should the pricer report it?

## Decision drivers

* "Missing price" is business data, not a runtime fault; using exceptions to carry it inverts their purpose.
* The caller needs the complete set of problem items to act on (resolve prices and retry), not just the first failure.
* Failures must not leave a half-computed result that callers can misread.

## Considered options

* **Throw an exception on the first missing price** — hides business meaning, loses the full list, forces `try/catch` for a normal flow.
* **Return a nullable subtotal** — a `null` result carries no information about what went wrong.
* **Return an explicit `CartPricingResult`** — either priced (a subtotal) or failed (the complete list of unpriced sellable items).

## Decision outcome

`ShoppingCartPricer` returns a `CartPricingResult` that is either *priced*, carrying the subtotal, or *failed*, carrying every sellable item without a price. The pricer never throws for expected pricing failures and never returns a partial, silently-incomplete result.

### Consequences

* Good: success and failure are both values the caller can pattern-match on; the full failure list enables batch recovery.
* Good: no exceptions for control flow; the signature states all possible outcomes.
* Bad: callers must inspect the result rather than assuming a subtotal — enforced by design, not by convention.
