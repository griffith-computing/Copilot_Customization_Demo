---
description: "Review a code change for security issues, validation gaps, and sensitive-data leakage"
agent: "Security Reviewer"
argument-hint: "Feature, endpoint, or file path under review (e.g. TicketEndpoints.cs or support-ticket)"
---

Perform a security review of {{input}} and assess the code before it is merged.

1. Inspect the implementation in `src/SupportApi/` and any related tests in `tests/SupportApi.Tests/`.
2. Check the repository standards in `.github/copilot-instructions.md` and the relevant customization assets in `.github/skills/`.
3. Evaluate:
   - validation and abuse resistance,
   - authentication/authorization boundaries,
   - logging and privacy protections,
   - exposure of secrets or sensitive data,
   - test coverage for dangerous inputs and failure modes.
4. Provide a structured output:
   - Executive Summary
   - Findings by Severity
   - Recommended Fixes
   - Missing Tests

Focus on actionable risk, evidence, and precise remediation guidance. Do not modify files.
