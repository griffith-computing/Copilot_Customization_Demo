# Demo Catalog

This repository supports two customer-facing demo flows that share the same underlying ASP.NET Core sample app.

## 1. Support API feature delivery demo
- Focus: build and validate an endpoint using GitHub Copilot conventions.
- Entry point: [../DEMO_WALKTHROUGH.md](../DEMO_WALKTHROUGH.md)
- Supporting assets: [../.github/copilot-instructions.md](../.github/copilot-instructions.md), [../.github/skills/api-feature-delivery](../.github/skills/api-feature-delivery), [../.github/agents/api-reviewer.agent.md](../.github/agents/api-reviewer.agent.md)

## 2. Security review demo
- Focus: use GitHub Copilot as a specialist reviewer for secure code assessment.
- Entry point: [security-review/README.md](security-review/README.md)
- Supporting assets: [../.github/skills/security-review](../.github/skills/security-review), [../.github/agents/security-reviewer.agent.md](../.github/agents/security-reviewer.agent.md), [../.github/prompts/review-security-change.prompt.md](../.github/prompts/review-security-change.prompt.md)
- Scenario setup: run `.\security-review\stage-demo.ps1` from this folder, then use `.\security-review\reset-demo.ps1` after the review.

## Shared application sample
- ASP.NET Core Minimal API: `src/SupportApi/`
- Integration tests: `tests/SupportApi.Tests/`
- Validation and security guidance: `.github/copilot-instructions.md`

The security-review flow is standalone: its staging script creates the deliberate review target, so presenters do not need to complete the feature-delivery demo first.
