# 0003 — Use tests as the executable specification of the domain

* Status: Accepted
* Date: 2026-09-13

## Context and problem statement

With no database, HTTP, or UI in V1, nothing outside the test suite can demonstrate that the domain behaves as specified. How should the behavior of the domain be specified and verified?

## Decision drivers

* Rules must be demonstrable without any infrastructure.
* The project is a learning exercise: rules should be written as failing tests first, then implemented.
* Tests are the most durable documentation of behavior — they cannot silently drift from the code.

## Considered options

* **Specification documents only** — prose cannot prove correctness.
* **Tests written after implementation** — verification only, no design feedback.
* **Test-first, tests as specification** — each rule of the domain is expressed as a failing test, then made to pass.

## Decision outcome

Tests are the executable specification of the domain. Prefer expressing any new rule as a failing test first. Each test documents one behavior of the domain; the test suite doubles as the behavioral reference for `Commerce.Domain`.

### Consequences

* Good: behavior is provable and reviewable; regressions are caught immediately.
* Good: writing the test first forces clarity about what the rule actually is.
* Bad: the test suite requires ongoing care to stay readable as specification, not just green.
