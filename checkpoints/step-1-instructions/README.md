# Checkpoint Step 1: Baseline with Custom Instructions

At this stage, the project contains:
- `SupportApi.slnx` with `src/SupportApi` and `tests/SupportApi.Tests`.
- Baseline `FeedbackEndpoints.cs` showing established patterns.
- `.github/copilot-instructions.md` containing repository conventions.

## Demo Action
Run the prompt:
> Add an endpoint to create a support ticket. Follow the existing project patterns.

## Expected Result
Copilot reads `.github/copilot-instructions.md` and generates:
- `Ticket.cs` and `CreateTicketRequest.cs` record types.
- `TicketEndpoints.cs` with `MapTicketEndpoints` extension method.
- Validation logic returning `TypedResults.ValidationProblem`.
- Structured logging recording ONLY `TicketId` and priority, omitting description.
- Corresponding integration tests in `TicketEndpointsTests.cs`.
