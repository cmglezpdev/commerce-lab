# 0000 — Record architecture decisions

* Status: Accepted
* Date: 2026-09-18

## Context and problem statement

The project is a learning exercise that aims to demonstrate how a well-run team builds software. As the application grows — from a pure domain model to application layer, infrastructure, and cloud deployment — design decisions will accumulate. If decisions stay in chat logs, commit messages, or someone's head, the reasoning behind the architecture becomes untraceable and cannot be reviewed or learned from.

## Decision drivers

* Decisions should be reviewable at any point in time, including months later.
* New contributors (human or AI) should be able to understand *why* the model looks the way it does.
* Reversing a decision should be as visible as making one.
* Low friction: writing an ADR must be cheaper than not writing one.

## Considered options

* **No records** — decisions live in commits and code only.
* **A design wiki** — free-form pages, hard to keep consistent.
* **ADRs in MADR format** — one Markdown file per decision, numbered, immutable by number.

## Decision outcome

We record significant architecture decisions as ADRs in `docs/adr/`, using a lean [MADR](https://adr.github.io/madr/) format. Each ADR has a sequential number that never changes. When a decision is reversed, a new ADR supersedes the old one and the old one is marked as `Superseded`.

### Consequences

* Good: the design history of the project is reviewable end to end.
* Good: ADRs double as learning material, which is part of the project's purpose.
* Bad: every significant decision carries a small documentation cost.
