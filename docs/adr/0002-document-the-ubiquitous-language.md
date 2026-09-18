# 0002 — Document the ubiquitous language in a versioned file

* Status: Accepted
* Date: 2026-09-13

## Context and problem statement

The domain distinguishes concepts that casual vocabulary collapses: a *product* (a catalog entry) is not a *sellable item* (a purchasable unit) is not a *cart line*. Without a written, agreed vocabulary, naming drifts, aggregates absorb wrong responsibilities, and discussions about the model become ambiguous. Where does the shared vocabulary live?

## Decision drivers

* The vocabulary is a first-class part of the domain, not informal chat material.
* The same terms must mean the same thing across code, tests, and documentation.
* Ambiguity like "product" meaning three different things produces unmaintainable models.

## Considered options

* **No formal vocabulary** — rely on conversation and code reading.
* **Glossary scattered across ADRs and comments** — fragmented, no single source of truth.
* **One versioned domain-language file** — a living document defining each term, its meaning, and terms to avoid.

## Decision outcome

The ubiquitous language lives in `docs/domain-language.md`. It defines each domain term, contrasts it with terms to avoid, and evolves with the model. Domain discussions, code naming, tests, and documentation are expected to use these terms consistently.

### Consequences

* Good: a single source of truth for naming; disagreements about a term are resolved by editing one file.
* Good: the file is itself a demonstration of DDD's language-first approach.
* Bad: it must be maintained; a stale language document is worse than none.
