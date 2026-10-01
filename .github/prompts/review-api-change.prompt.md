---
description: "Review an API endpoint change for validation, security, and test coverage"
agent: "API Reviewer"
argument-hint: "Endpoint name or file path (e.g. TicketEndpoints.cs)"
---

Perform a comprehensive review of the API endpoint change for {{input}}:

1. Inspect the endpoint implementation in `src/SupportApi/Endpoints/` and its models in `src/SupportApi/Models/`.
2. Inspect the associated integration tests in `tests/SupportApi.Tests/`.
3. Check against our repository standards in `.github/copilot-instructions.md`:
   - Proper Minimal API endpoint structure and typed results.
   - Strict RFC 7807 validation error format (`ValidationProblem`).
   - Security rule: NO sensitive data (PII, user descriptions, comments) logged.
   - Comprehensive test coverage for both success (201 Created) and failure (400 Bad Request) paths.

Output your review using the standard structure:
- **Executive Summary**
- **Findings by Severity** (with file and line references)
- **Recommended Fixes**
- **Missing Tests**
