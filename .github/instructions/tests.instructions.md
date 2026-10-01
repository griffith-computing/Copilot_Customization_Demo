---
applyTo: "tests/**/*.cs"
description: "Integration testing conventions for SupportApi test fixtures"
---

# Integration Test Instructions

Apply these guidelines whenever creating, refactoring, or running tests in `tests/`:

## Test Class Structure
- Every test class must inherit from `IClassFixture<WebApplicationFactory<Program>>`.
- Inject `WebApplicationFactory<Program>` via constructor and initialize `HttpClient` via `factory.CreateClient()`.
- Use the standard naming convention: `<Action>_With<Scenario>_Returns<ExpectedResult>` (e.g. `CreateTicket_WithInvalidPayload_Returns400ValidationProblem`).

## Assertions & Coverage
- Always assert status codes using explicit enum values from `System.Net.HttpStatusCode` (e.g., `Assert.Equal(HttpStatusCode.Created, response.StatusCode)`).
- For HTTP 201 Created:
  - Assert that `response.Headers.Location` is not null.
  - Deserialize the JSON body and verify required identifiers are populated.
- For HTTP 400 Bad Request:
  - Verify `response.Content.Headers.ContentType?.MediaType` equals `"application/problem+json"`.
  - Validate that validation error details are populated.
