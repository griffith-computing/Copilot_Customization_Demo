---
name: security-review
description: 'Security review procedure for application code, API endpoints, and validation logic. Use when auditing new or changed code for vulnerabilities and unsafe patterns.'
argument-hint: 'Feature, endpoint, or file path to assess (e.g. support-ticket or TicketEndpoints.cs)'
user-invocable: true
---

# Security Review Skill

Use this skill when a developer has created or changed code and you need a structured, security-focused review before it ships.

## When to Use
- Reviewing a new endpoint or API feature for high-risk issues.
- Auditing validation, authorization, and error handling.
- Checking whether logging or response payloads leak sensitive data.
- Verifying test coverage around dangerous inputs and failure modes.

## Procedure

### Step 1: Define Scope
- Identify the files changed or the feature under review.
- Check the endpoint or component entry points and any supporting models or services.
- Read the repository guidance in `.github/copilot-instructions.md` before judging compliance.

### Step 2: Review Attack Surface and Trust Boundaries
- Look for user-controlled inputs and data flow boundaries.
- Check whether there are missing authorization or authentication checks.
- Confirm secrets, tokens, and configuration values are not embedded in code or logs.

### Step 3: Validate Input Handling
- Ensure validation is explicit and defensive.
- Confirm invalid input returns standardized RFC 7807 ProblemDetails responses.
- Look for missing length, format, range, or enum validation in request models.

### Step 4: Inspect Security Hygiene
- Verify no customer descriptions, ticket bodies, credentials, or PII are logged.
- Check for stack traces or raw exception details in production responses.
- Review for unsafe defaults, permissive CORS rules, or weak error handling patterns.

### Step 5: Review Test Coverage
- Ensure success and failure flows are tested.
- Include edge cases for invalid input, unauthorized access, and boundary conditions.
- Check whether the tests validate the security behavior that matters.

### Step 6: Summarize Findings
- Report findings by severity with file references and evidence.
- Recommend precise remediation steps and note any missing tests.
- Keep the review objective and actionable.

## Quality Bar
A review is only complete when it clearly identifies:
- the risk,
- where it appears,
- why it is dangerous,
- and how to fix or validate it.
