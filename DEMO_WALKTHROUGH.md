# Demo Walkthrough: "From Feature Request to Reviewed Code"

A cohesive 10–12 minute live demonstration showcasing how the four GitHub Copilot customization primitives work together in a single ASP.NET Core Minimal API workflow.

> **Key Takeaway:**
> *"Instructions set the rules. Skills provide the know-how. Agents define the specialist. Prompt files launch the task."*

---

## Demo Timing & Agenda (10–12 Minutes)

| Time | Stage | Customization Primitive | Presenter Goal |
|---|---|---|---|
| **00:00 - 02:00** | Introduction & Context | Baseline Repo | Show running API and established patterns in `.github/copilot-instructions.md` |
| **02:00 - 04:30** | Step 1: Feature Request | **Custom Instructions** | Prompt Copilot to add ticket endpoint; show rules automatically followed |
| **04:30 - 07:00** | Step 2: Delivery Procedure | **Skill** | Invoke `/api-feature-delivery` to complete endpoint & automated tests |
| **07:00 - 09:30** | Step 3: Specialist Review | **Custom Agent** | Switch to `@API Reviewer` to inspect the code with bounded read-only tools |
| **09:30 - 11:00** | Step 4: Repeatable Launch | **Prompt File** | Run `/review-api-change` to generate standardized findings & remediation |
| **11:00 - 12:00** | Summary & Conclusion | Architecture Recap | Reiterate the 4 primitives distinction |

---

## Preparation & Prerequisites

1. Open this repository in VS Code:
   ```powershell
   code .
   ```
2. Verify .NET 8 SDK is available and tests pass:
   ```powershell
   dotnet test SupportApi.slnx
   ```
3. Open GitHub Copilot Chat in the sidebar (`Ctrl+Alt+I` / `Cmd+Alt+I`).
4. Keep the `checkpoints/` folder available in case live generation stalls.

---

## Step-by-Step Script

### Step 0: Baseline & Instructions (00:00 - 02:00)

**Presenter Talk Track:**
> *"Every engineering team has conventions: how endpoints are routed, how validation errors are returned, what we are never allowed to log, and how tests must be written. Instead of repeating those rules in every prompt, we establish repository instructions."*

1. Open `.github/copilot-instructions.md`.
2. Highlight the 4 core sections:
   - **Architecture:** ASP.NET Core Minimal APIs with typed results (`Results<Created<T>, ValidationProblem>`).
   - **Standard Responses:** RFC 7807 ProblemDetails for validation failures (`TypedResults.ValidationProblem`).
   - **Security Rule:** Strictly NO logging of customer descriptions, ticket bodies, or PII.
   - **Testing:** Automated integration tests with `WebApplicationFactory<Program>`.
3. Briefly show `src/SupportApi/Endpoints/FeedbackEndpoints.cs` to show existing code adhering to these rules.

---

### Step 1: Custom Instructions in Action (02:00 - 04:30)

**Presenter Talk Track:**
> *"Now let's request a new feature: a support ticket creation endpoint. Notice how short the prompt is—we don't need to specify our architecture, return types, or error shapes."*

1. Open Copilot Chat.
2. Enter the prompt:
   ```text
   Add an endpoint to create a support ticket. Follow the existing project patterns.
   ```
3. When Copilot generates the code, highlight what was automatically respected:
   - Uses `record Ticket` and `record CreateTicketRequest`.
   - Uses `MapTicketEndpoints` extension method.
   - Returns `Results<Created<Ticket>, ValidationProblem>`.
   - Returns RFC 7807 dictionary validation errors.
   - Logs only `ticket.Id` or priority—**not** the user's ticket description.

> **Takeaway:** *Instructions define the rules that apply across all work.*

*(Fallback: If you need the exact code immediately, copy from `checkpoints/step-2-skill/TicketEndpoints.cs`)*

---

### Step 2: Skill — "Use Our Repeatable Delivery Procedure" (04:30 - 07:00)

**Presenter Talk Track:**
> *"Project rules tell Copilot WHAT the standards are. But real engineering workflows require a multi-step procedure: inspecting neighboring code, implementing request validation bounds, mapping the route, generating tests, and running the checklist. That's what a Skill provides."*

1. Show `.github/skills/api-feature-delivery/`:
   - `SKILL.md`: The 5-step delivery procedure.
   - `references/checklist.md`: The delivery criteria.
   - `assets/test-template.cs`: Standard integration test structure.
2. In Copilot Chat, run:
   ```text
   Use the API Feature Delivery skill to finish this endpoint and its tests.
   ```
3. Copilot executes the 5-step procedure:
   - Checks `InMemoryStore.cs` and `TicketEndpoints.cs`.
   - Generates `tests/SupportApi.Tests/TicketEndpointsTests.cs` using the template.
   - Validates both 201 Created and 400 BadRequest with ProblemDetails.
4. Run tests in terminal:
   ```powershell
   dotnet test SupportApi.slnx
   ```

> **Takeaway:** *A skill packages reusable know-how and supporting assets, not just coding rules.*

*(Fallback: If needed, copy `checkpoints/step-2-skill/TicketEndpointsTests.cs` into `tests/SupportApi.Tests/`)*

---

### Step 3: Custom Agent — "Bring in a Specialist" (07:00 - 09:30)

**Presenter Talk Track:**
> *"Now we need an objective, independent assessment before merging. But we don't want a generic assistant that might quietly edit files or run risky shell commands. We want a bounded specialist: the API Reviewer."*

1. Open `.github/agents/api-reviewer.agent.md`.
2. Point out:
   - `tools: [read, search]` — Strictly read-only; no `edit` or `execute` tools.
   - Explicit instructions stating it must never edit files.
3. Introduce a deliberate security violation to demonstrate detection:
   - Open `src/SupportApi/Endpoints/TicketEndpoints.cs` (or copy from `checkpoints/step-3-deliberate-bug/TicketEndpoints.cs`).
   - Notice the deliberate bug:
     ```csharp
     logger.LogInformation("Ticket {TicketId} created with description: {Description}", ticket.Id, request.Description);
     ```
4. In Copilot Chat, select `@API Reviewer` (or type `@API Reviewer`):
   ```text
   Review the support-ticket endpoint for validation, security, and test coverage. Report findings; do not change files.
   ```
5. Observe the specialist's response:
   - Pinpoints the security leak (logging `Description` violates log privacy).
   - Identifies any validation edge cases.
   - Does **not** attempt to modify any files.

> **Takeaway:** *A custom agent defines who is doing the work, their approach, and their boundaries.*

---

### Step 4: Prompt File — "Make the Review a Repeatable Action" (09:30 - 11:00)

**Presenter Talk Track:**
> *"Typing a complex prompt every time invites inconsistency across engineers. A Prompt File turns this specialized task into a one-click or single-slash command with standardized outputs."*

1. Open `.github/prompts/review-api-change.prompt.md`.
2. Point out:
   - Frontmatter specifies `agent: "API Reviewer"`.
   - Structures the report: Findings by severity, file/line locations, recommended fixes, missing tests.
3. In Copilot Chat, run:
   ```text
   /review-api-change TicketEndpoints.cs
   ```
4. Copilot automatically selects the API Reviewer agent and outputs the structured report.
5. Apply the recommended remediation (or copy from `checkpoints/step-4-reviewed-fix/TicketEndpoints.cs`).

> **Takeaway:** *A prompt file defines a task you explicitly launch, with consistent context and output requirements.*

---

### Closing Summary (11:00 - 12:00)

Conclude the presentation with the core mental model:

| Customization | Mental Model | What it does in the Demo |
|---|---|---|
| **Instructions** | *The Rules* | Sets project-wide patterns (Minimal APIs, no PII logging, RFC 7807) |
| **Skill** | *The Know-How* | Guides multi-step delivery with checklists and test templates |
| **Custom Agent** | *The Specialist* | Restricts tools to read-only inspection for safe auditing |
| **Prompt File** | *The Launch Action* | Packages the review into a repeatable, parameterized command |

> **"Instructions set the rules. Skills provide the know-how. Agents define the specialist. Prompt files launch the task."**
