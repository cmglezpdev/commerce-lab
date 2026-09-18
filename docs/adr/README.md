# Architecture Decision Records

This directory records the significant architecture decisions of the project, in [MADR](https://adr.github.io/madr/) format (lean).

Each decision gets one file: `NNNN-title-with-dashes.md`, numbered sequentially. Once a number is used, it never changes — if a decision is reversed, we write a new ADR that supersedes it and update the status of the old one.

## Index

| ADR | Title | Status |
| --- | --- | --- |
| [0000](0000-record-architecture-decisions.md) | Record architecture decisions | Accepted |
| [0001](0001-start-with-a-single-pure-domain-assembly.md) | Start with a single pure domain assembly | Accepted |
| [0002](0002-document-the-ubiquitous-language.md) | Document the ubiquitous language in a versioned file | Accepted |
| [0003](0003-use-tests-as-the-executable-specification.md) | Use tests as the executable specification of the domain | Accepted |
| [0004](0004-price-the-cart-with-a-stateless-domain-service.md) | Price the cart with a stateless domain service | Accepted |
| [0005](0005-return-an-explicit-pricing-result-instead-of-throwing.md) | Return an explicit pricing result instead of throwing | Accepted |
| [0006](0006-model-sellable-items-as-an-explicit-shared-identity.md) | Model sellable items as an explicit shared identity | Accepted |

## Format

Each ADR contains: **Context and problem statement**, **Decision drivers**, **Considered options**, **Decision outcome** (with rationale), and **Consequences**.
