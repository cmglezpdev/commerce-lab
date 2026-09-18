# 0001 — Start with a single pure domain assembly

* Status: Accepted
* Date: 2026-09-13

## Context and problem statement

The project starts as a learning exercise in C# and Domain-Driven Design. Building database, API, and UI at the same time would scatter attention across infrastructure concerns and let the domain model degrade into an anemic pass-through layer. How should the project begin?

## Decision drivers

* The core learning goal is domain modeling: aggregates, invariants, value objects, domain services.
* Framework code (EF Core, ASP.NET, HTTP) dominates tutorials and hides the design work.
* The domain must later survive contact with infrastructure without being rewritten.

## Considered options

* **Full stack from day one** — a vertical slice with DB, API, and UI.
* **Domain first, single assembly** — one `Commerce.Domain` assembly plus its test project; everything else comes later.
* **Multiple bounded-context assemblies from the start** — premature boundaries.

## Decision outcome

V1 consists of a single assembly, `Commerce.Domain`, with modules `Catalog`, `Cart`, and `Common` (promotions planned). No EF Core, SQL, HTTP, ASP.NET, DTOs, or concrete repositories are used in this stage.

The modules are candidates for future bounded contexts, not claimed as such: a bounded context delimits a coherent model and language, and we do not assert that boundary before the model proves it.

### Consequences

* Good: all effort goes into modeling and defending invariants; tests run instantly with no infrastructure.
* Good: the domain is portable — application and infrastructure layers can be added later without rewriting its rules.
* Bad: some questions (SKU uniqueness, bundle component existence) cannot be enforced inside a single aggregate and must be deferred to a future query port or application layer.
