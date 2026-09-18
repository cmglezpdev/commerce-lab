# Commerce Lab

A complete commerce application built the deliberate way: it starts as a pure C# domain model using Domain-Driven Design, and grows step by step into a full application — application layer, infrastructure, database, API, and cloud deployment with Terraform.

> **This is a learning project.** Its goal is to practice building software the way well-run teams do it — with a real ubiquitous language, protected invariants, design patterns used with intent, and architecture decisions recorded and justified — not to ship a store you can copy-paste.

## Why this exists

Most tutorials hand you a finished stack and skip the decisions. This project goes the other way: every piece of design is discussed, tested, and documented, and every architecture decision gets an ADR. As the application grows, the repository becomes a living record of *how* a system is built — not just *that* it exists.

The learning goals:

- **C#** — idiomatic, modern language features used where they earn their place
- **Domain-Driven Design** — aggregates, value objects, domain services, bounded context thinking
- **Design patterns & OOP** — encapsulation, composition, inheritance and polymorphism with intent
- **Architecture** — layering, ports, and how a domain survives contact with infrastructure
- **Infrastructure & cloud** — databases, API, and deployment with Terraform (AWS, with Azure variants for practice)

## What it is today

The current stage is a **pure domain model**: a single assembly, `Commerce.Domain`, with modules `Catalog`, `Cart`, and `Common`. No database, no HTTP, no UI — business rules are expressed entirely through tests.

- **Catalog** — simple products, variable products with options and variants, and bundles
- **Cart** — a shopping cart that protects its own invariants, and a stateless cart pricer with an explicit pricing result
- **Common** — value objects like `Money`, `Currency`, `Quantity`, `Sku`

```
src/
  Commerce.Domain/
    Catalog/    Products, variants, bundles, sellable items
    Cart/       Shopping cart, cart pricer, pricing result
    Common/     Shared value objects
tests/
  Commerce.Domain.Tests/    The executable specification of the domain
docs/
  domain-language.md        The ubiquitous language (source of truth)
  adr/                      Architecture decision records (MADR)
```

## Getting started

```bash
dotnet build Commerce.slnx
dotnet test Commerce.slnx
```

That's all you need — the domain has no external dependencies. Read the tests to see the rules of the domain in action; read `docs/adr/` to understand why the model looks the way it does.

## Documentation

- [`docs/domain-language.md`](docs/domain-language.md) — the ubiquitous language: what a *sellable item*, a *bundle*, or a *cart pricer* means here
- [`docs/adr/`](docs/adr/README.md) — every significant architecture decision, in MADR format

## Roadmap

- [x] Pure domain model: catalog, cart, pricing
- [ ] Promotions module
- [ ] Application layer (use cases, ports)
- [ ] Infrastructure: persistence with a real database
- [ ] HTTP API
- [ ] Cloud deployment with Terraform (AWS; Azure variants)
