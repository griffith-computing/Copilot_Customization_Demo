# Copilot Customization Demo: Security Review and API Delivery

This repository is a customer-ready demo for showcasing how GitHub Copilot can be customized for both feature delivery and security review workflows. It uses a practical ASP.NET Core sample so the audience can see real developer work, review rules, and remediation guidance in context.

## Overview

The repo now separates the demo into clear customer-facing tracks:

1. Support API feature delivery demo: build and validate a new endpoint using authoritative project conventions.
2. Security review demo: use a dedicated reviewer agent and skill to assess code for validation gaps, log leakage, and missing safeguards.

The project uses the same underlying sample app for both scenarios so the story stays coherent:

- Custom Instructions: `.github/copilot-instructions.md`
- Feature skill: `.github/skills/api-feature-delivery/`
- Security skill: `.github/skills/security-review/`
- API reviewer agent: `.github/agents/api-reviewer.agent.md`
- Security reviewer agent: `.github/agents/security-reviewer.agent.md`
- Prompt files: `.github/prompts/`

> "Instructions set the rules. Skills provide the know-how. Agents define the specialist. Prompt files launch the task."

---

## Quick Start

1. Install .NET 8 SDK.
2. Open the repo in VS Code.
3. Verify the baseline project passes:
   ```powershell
   dotnet test SupportApi.slnx
   ```
4. Choose the demo track:
   - [demos/support-api/README.md](demos/support-api/README.md): feature-delivery walkthrough for the sample support API
   - [demos/security-review/README.md](demos/security-review/README.md): standalone security review workflow with guarded stage/reset scripts
   - [DEMO_WALKTHROUGH.md](DEMO_WALKTHROUGH.md): quick 10–12 minute demo script
   - [DEMO_WALKTHROUGH_45MIN.md](DEMO_WALKTHROUGH_45MIN.md): extended workshop version

## Demo folder layout

```text
├── .github/
│   ├── copilot-instructions.md
│   ├── agents/
│   │   ├── api-reviewer.agent.md
│   │   └── security-reviewer.agent.md
│   ├── instructions/
│   │   └── tests.instructions.md
│   ├── prompts/
│   │   ├── review-api-change.prompt.md
│   │   └── review-security-change.prompt.md
│   └── skills/
│       ├── api-feature-delivery/
│       └── security-review/
├── demos/
│   ├── README.md
│   ├── support-api/
│   │   └── README.md
│   └── security-review/
│       ├── README.md
│       ├── stage-demo.ps1
│       └── reset-demo.ps1
├── src/
│   └── SupportApi/
├── tests/
│   └── SupportApi.Tests/
├── checkpoints/
├── DEMO_WALKTHROUGH.md
├── DEMO_WALKTHROUGH_45MIN.md
├── README.md
├── SupportApi.slnx
└── .gitignore
```

## Recommended customer narrative

- Show the sample app as the code developers create.
- Explain the project instructions that shape safe API design and testing.
- Use the feature-delivery skill to accelerate creating a validated endpoint.
- For the standalone security track, stage the deliberate review target with `.\demos\security-review\stage-demo.ps1`.
- Switch to the security-review agent to assess the code before review or merge.
- End by showing the repeatable `/review-security-change` prompt as the standardized review workflow.

This gives customers a clear story: GitHub Copilot is not just a coding assistant, it is also a governed security review partner for their engineering process.
