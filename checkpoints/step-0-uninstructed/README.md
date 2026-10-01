# Step 0: Uninstructed Baseline (The "Before" Contrast)

This checkpoint illustrates what happens when an LLM / Copilot is asked to generate code **without custom instructions or repository context**.

## The Uninstructed Prompt
```text
Add an endpoint to create a support ticket.
```

## Typical Common Architectural & Security Drifts
When unguided by repository instructions, the AI tends to generate code with common anti-patterns:
1. **Controller vs. Minimal API**: Generates a standard ASP.NET Core `ControllerBase` class rather than continuing the project's Minimal API extension methods.
2. **Raw Error Messages**: Returns `BadRequest("Email is invalid")` instead of RFC 7807 `application/problem+json` typed validation problems.
3. **Sensitive Data Leakage**: Injects `logger.LogInformation("Creating ticket: {Title} - {Description}", ...)` directly into logs, leaking customer comments and PII.
4. **Missing or Inconsistent Tests**: Writes unit tests that mock everything rather than integration tests with `WebApplicationFactory<Program>`, or omits HTTP 400 validation assertions.

See [TicketEndpoints_Drifted.cs](./TicketEndpoints_Drifted.cs) for a representative sample of drifted code to show during a presentation.
