# AGENTS.md

## Project

**Commerce** — a commerce application built as a structured C# learning project.
It starts as a pure domain model (DDD) and grows incrementally into a full application: application layer, infrastructure, database, API, and cloud deployment with Terraform (AWS/Azure).

The current stage is a single assembly, `Commerce.Domain`, with modules `Catalog`, `Cart`, and `Common` (promotions are planned). Business rules are expressed and documented through tests — no database, HTTP, or UI.

## Ubiquitous language

The source of truth for domain terminology is **`docs/domain-language.md`**. It replaces the legacy root `CONTEXT.md`.
When naming domain concepts, reviewing the model, or writing domain docs, treat that file as the ubiquitous-language reference.

## Conventions

- Public documentation (README, `docs/`) is written in **English**. Keep it that way.
- Learning material (`lessons/`, `learning-records/`, `reference/`, `PRD.md`, `MISSION.md`, etc.) is intentionally git-ignored. Never commit it.
- Domain modeling decisions are recorded as ADRs in `docs/adr/` using MADR format.

## Commands

```bash
dotnet build Commerce.slnx
dotnet test Commerce.slnx
```

Tests are the executable specification of the domain: prefer expressing a new rule as a failing test first.
