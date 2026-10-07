# Security Review Demo

This standalone demo shows how GitHub Copilot can assess developer-created code with repository instructions, a reusable security-review skill, a read-only custom agent, and a repeatable prompt file.

The scenario stages an intentionally vulnerable support-ticket endpoint under `src/SupportApi/Endpoints/`. The endpoint logs customer-provided ticket content, giving the reviewer a concrete security and privacy issue to identify.

## What the demo covers

- Repository instructions that define secure API and logging rules
- A reusable skill that provides a consistent assessment procedure
- A read-only custom agent that performs specialist security analysis
- A prompt file that standardizes the review request and output

## Environment preparation

1. Install:
   - [Visual Studio Code](https://code.visualstudio.com/)
   - [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
   - GitHub Copilot and GitHub Copilot Chat extensions
2. Open the repository root in VS Code:
   ```powershell
   code .
   ```
3. Confirm the baseline tests pass:
   ```powershell
   dotnet test SupportApi.slnx
   ```
4. Open Copilot Chat and confirm the repository customization assets are available:
   - `.github/copilot-instructions.md`
   - `.github/skills/security-review/SKILL.md`
   - `.github/agents/security-reviewer.agent.md`
   - `.github/prompts/review-security-change.prompt.md`

## Stage the review scenario

From the repository root, run:

```powershell
& .\demos\security-review\stage-demo.ps1
```

The script copies `checkpoints/step-3-deliberate-bug/TicketEndpoints.cs` to:

```text
src/SupportApi/Endpoints/TicketEndpoints.cs
```

It will not overwrite a different file at that path. After staging, verify the sample still compiles and its baseline tests pass:

```powershell
dotnet test SupportApi.slnx
```

Open `src/SupportApi/Endpoints/TicketEndpoints.cs` and keep it visible during the review.

## Run the customer workflow

### 1. Show the governing instructions

Open `.github/copilot-instructions.md` and highlight the security rule prohibiting logs that contain ticket descriptions, customer content, credentials, or PII.

### 2. Use the Security Reviewer agent

Select the **Security Reviewer** custom agent in Copilot Chat, then enter:

```text
Review src/SupportApi/Endpoints/TicketEndpoints.cs for validation, access control, logging, privacy, and missing security tests. Report findings with file and line evidence. Do not change files.
```

Highlight that the agent is configured with read/search tools only and reports findings without editing developer code.

### 3. Invoke the reusable skill

In Copilot Chat, enter:

```text
Use the security-review skill to assess src/SupportApi/Endpoints/TicketEndpoints.cs and its related models and tests.
```

The skill guides Copilot through scope, trust boundaries, input handling, logging hygiene, test coverage, and severity-based reporting.

### 4. Run the repeatable prompt

Run:

```text
/review-security-change src/SupportApi/Endpoints/TicketEndpoints.cs
```

The prompt selects the Security Reviewer and requests the same structured assessment for every developer change.

## Expected review result

The primary finding should identify this log statement as a sensitive-data exposure:

```csharp
logger.LogInformation("Ticket {TicketId} created with description: {Description}", ticket.Id, request.Description);
```

A strong review should explain that:

- `request.Description` is customer-controlled ticket content;
- the repository explicitly prohibits logging ticket bodies and customer descriptions;
- logs often have broader access and longer retention than application data;
- remediation should log only non-sensitive metadata such as `TicketId` and `Priority`;
- tests or logging verification should guard against future regressions.

The reviewer may also note missing authorization decisions or security-focused boundary tests, but it should distinguish confirmed vulnerabilities from design questions that require more context.

## Reset the repository

Before continuing with another demo, remove the staged endpoint:

```powershell
& .\demos\security-review\reset-demo.ps1
dotnet test SupportApi.slnx
```

The reset script deletes the file only when it exactly matches the deliberate-bug checkpoint. If the file was edited during the demo, the script stops instead of deleting those changes.

## Presenter talking points

- **Instructions are the rules:** they define non-negotiable security and engineering standards.
- **Skills are the procedure:** they make the assessment systematic and reusable.
- **Agents are the specialist:** they establish a security persona and bounded, read-only tools.
- **Prompt files are the launch action:** they give every developer a consistent way to request the review.

## Related documentation

- [Demo catalog](../README.md)
- [Support API feature-delivery demo](../support-api/README.md)
- [Repository overview](../../README.md)

