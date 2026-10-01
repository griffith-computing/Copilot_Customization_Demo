# API Feature Delivery Checklist

Verify every delivered endpoint against this checklist before marking the feature complete:

## 1. Architecture & Design
- [ ] Endpoint defined as an extension method on `IEndpointRouteBuilder` in `SupportApi.Endpoints`.
- [ ] Route group mapped with relevant tags (e.g., `.WithTags("Tickets")`).
- [ ] Request and response models defined as C# `record` types in `SupportApi.Models`.
- [ ] Endpoint group registered in `src/SupportApi/Program.cs`.

## 2. Validation & Error Handling
- [ ] Mandatory fields verified for null, empty, or whitespace.
- [ ] Domain-specific constraints enforced (e.g., email format, minimum text lengths, allowed enum/priority values).
- [ ] Errors returned using RFC 7807 `TypedResults.ValidationProblem(errors)` with HTTP status 400.
- [ ] Return type signature explicitly declares `Results<Created<T>, ValidationProblem>`.

## 3. Security & Logging
- [ ] Structured logging used (`ILoggerFactory` or `ILogger`).
- [ ] No sensitive request bodies, descriptions, or PII included in log messages.
- [ ] Only resource IDs, categories, priorities, and status values logged.

## 4. Automated Testing
- [ ] Test class inherits `IClassFixture<WebApplicationFactory<Program>>`.
- [ ] Success test verifies HTTP 201 Created and response payload.
- [ ] Validation test verifies HTTP 400 Bad Request and `application/problem+json` content type.
- [ ] Full solution test suite passes via `dotnet test SupportApi.slnx`.
