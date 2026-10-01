# Copilot Customization Demo: From Feature Request to Reviewed Code

A complete, working ASP.NET Core demo repository demonstrating how all four GitHub Copilot customization primitives work together in a single end-to-end development workflow.

## Overview

Rather than disconnected toy examples, this repository uses one unified scenario—**adding and reviewing a support-ticket endpoint**—to demonstrate:

1. **Custom Instructions (`.github/copilot-instructions.md`)**: The rules that apply across the project (architecture, RFC 7807 error formats, security rules, and testing standards).
2. **Skill (`.github/skills/api-feature-delivery/`)**: The repeatable delivery procedure (inspection, validation, endpoint mapping, test generation, and checklists).
3. **Custom Agent (`.github/agents/api-reviewer.agent.md`)**: The specialist persona (a bounded, read-only reviewer with restricted tools).
4. **Prompt File (`.github/prompts/review-api-change.prompt.md`)**: The launchable task (a parameterized prompt invoking the specialist agent to produce a structured review report).

> **"Instructions set the rules. Skills provide the know-how. Agents define the specialist. Prompt files launch the task."**

---

## Quick Start

1. Build the solution and run existing integration tests:
   ```powershell
   dotnet test SupportApi.slnx
   ```
2. Choose your presentation guide:
   - [DEMO_WALKTHROUGH.md](DEMO_WALKTHROUGH.md): Rapid **10–12 minute** lightning demo format.
   - [DEMO_WALKTHROUGH_45MIN.md](DEMO_WALKTHROUGH_45MIN.md): Extended **45-minute** interactive workshop/deep-dive format (includes uninstructed drift contrast, scoped `applyTo` file instructions, progressive loading breakdown, tool sandboxing enforcement test, and live audience Q&A).

## Repository Structure

```
├── .github/
│   ├── copilot-instructions.md                  # Project rules (Rules)
│   ├── instructions/
│   │   └── tests.instructions.md                # Scoped file instructions with applyTo
│   ├── agents/
│   │   └── api-reviewer.agent.md                # Read-only audit specialist (Specialist)
│   ├── prompts/
│   │   └── review-api-change.prompt.md          # Launchable review task (Action)
│   └── skills/
│       └── api-feature-delivery/                # Multi-step workflow (Know-how)
│           ├── SKILL.md
│           ├── references/checklist.md
│           └── assets/
│               ├── example-endpoint.cs
│               └── test-template.cs
├── src/
│   └── SupportApi/                              # ASP.NET Core 8 Minimal API
│       ├── Endpoints/FeedbackEndpoints.cs       # Baseline endpoint pattern
│       ├── Models/Feedback.cs, Ticket.cs
│       ├── Services/InMemoryStore.cs
│       └── Program.cs
├── tests/
│   └── SupportApi.Tests/                        # xUnit integration tests
│       └── FeedbackEndpointsTests.cs
├── checkpoints/                                 # Ready-to-use fallbacks for demo stages
│   ├── step-0-uninstructed/                     # The "before" comparison showing architectural drift
│   ├── step-1-instructions/
│   ├── step-2-skill/
│   ├── step-3-deliberate-bug/                   # Deliberate logging leak for review
│   └── step-4-reviewed-fix/
├── DEMO_WALKTHROUGH.md                          # 10-12 minute lightning demo guide
├── DEMO_WALKTHROUGH_45MIN.md                    # 45-minute comprehensive workshop guide
└── SupportApi.slnx
```
