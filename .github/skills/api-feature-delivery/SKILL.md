---
name: api-feature-delivery
description: 'API Feature Delivery procedure for ASP.NET Core endpoints. Use when implementing or finishing an API endpoint, validating requests, and generating integration tests.'
argument-hint: 'Endpoint or feature name (e.g. support-ticket)'
user-invocable: true
---

# API Feature Delivery Skill

Repeatable delivery procedure for delivering ASP.NET Core Minimal API endpoints consistent with team conventions.

## When to Use
- Implementing a new API endpoint from scratch or feature request.
- Finishing a partially implemented endpoint with validation, logging, and tests.
- Ensuring compliance with team architecture, RFC 7807 error formats, security logging, and xUnit integration tests.

## Procedure

Follow this 5-step delivery procedure in sequence:

### Step 1: Inspect Neighboring Endpoints
- Review existing endpoints in `src/SupportApi/Endpoints/` (such as `FeedbackEndpoints.cs`) and reference templates in [assets/example-endpoint.cs](./assets/example-endpoint.cs).
- Confirm model records in `src/SupportApi/Models/` and store methods in `src/SupportApi/Services/InMemoryStore.cs`.

### Step 2: Implement Request Validation
- Validate all incoming request fields before mutating state.
- Collect all validation errors into a `Dictionary<string, string[]>`.
- On failure:
  - Log a warning with error count and category/identifier ONLY (no user text payloads).
  - Return `TypedResults.ValidationProblem(errors)` (`application/problem+json`).

### Step 3: Add Endpoint Mapping with Typed Results
- Define or update extension method `Map<Feature>Endpoints(this IEndpointRouteBuilder routes)`.
- Use explicit typed union returns: `Results<Created<T>, ValidationProblem>` or `Results<Ok<T>, NotFound, ValidationProblem>`.
- Register the endpoint group in `src/SupportApi/Program.cs`.
- Log creation events recording ONLY identifiers (e.g., `TicketId`), categories, and priorities. NEVER log descriptions or customer details.

### Step 4: Test Success and Invalid-Input Cases
- Consult [assets/test-template.cs](./assets/test-template.cs).
- Add integration test fixture in `tests/SupportApi.Tests/` using `WebApplicationFactory<Program>`.
- Verify:
  - Success scenario returns HTTP 201 Created with Location header and non-empty GUID.
  - Failure scenario returns HTTP 400 Bad Request with `application/problem+json` media type.
- Run `dotnet test SupportApi.slnx` to verify test suite passes.

### Step 5: Summarize Changes and Review Checklist
- Check off items against [references/checklist.md](./references/checklist.md).
- Report modified files, new routes, validation rules implemented, and test results.
