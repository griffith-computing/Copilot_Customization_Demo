# Support API Project Instructions

## Architecture & Code Style
- Use ASP.NET Core 8+ Minimal APIs with typed results (`Results<Created<T>, ValidationProblem>`, `Results<Ok<T>, NotFound>`).
- Define endpoint routing using static extension methods on `IEndpointRouteBuilder` in the `SupportApi.Endpoints` namespace (e.g., `MapFeedbackEndpoints`, `MapTicketEndpoints`).
- Use C# `record` types for DTOs and models with immutable properties (`record Feedback(...)`, `record CreateFeedbackRequest(...)`).
- Keep models in `SupportApi.Models`, services in `SupportApi.Services`, and endpoint definitions in `SupportApi.Endpoints`.

## Error Handling & Standard Responses
- Use RFC 7807 ProblemDetails for all error responses.
- Validation failures MUST return HTTP 400 Bad Request via `TypedResults.ValidationProblem(errors)` with dictionary mapping field names to error messages.
- Never return raw string error bodies or unformatted error payloads.

## Security & Logging Rules
- Sensitive data in logs is strictly prohibited:
  - NEVER log customer descriptions, ticket bodies, customer comments, credentials, or PII.
  - DO log resource IDs (e.g., `TicketId`, `FeedbackId`), status codes, priorities, and high-level categories.
  - Example good log: `logger.LogInformation("Ticket {TicketId} created with priority '{Priority}'.", ticket.Id, ticket.Priority);`
  - Example prohibited log: `logger.LogInformation("Ticket created with description: {Description}", request.Description);`

## Testing Requirements
- Every new endpoint must have automated integration tests in `tests/SupportApi.Tests/` using xUnit and `WebApplicationFactory<Program>`.
- Always provide test coverage for both:
  1. Success path (verifying HTTP 201 Created or HTTP 200 OK, response location header, and payload structure).
  2. Validation failure path (verifying HTTP 400 Bad Request with `application/problem+json` content type).
- Run `dotnet test SupportApi.slnx` to verify test suite status.
