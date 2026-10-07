# Support API Demo

This folder is the baseline sample used by the feature-delivery workflow. It demonstrates how GitHub Copilot can be guided with repository instructions, a reusable skill, and a specialist reviewer.

## What is in scope
- Minimal API endpoint examples in `src/SupportApi/`
- xUnit integration tests in `tests/SupportApi.Tests/`
- Review guidance in `.github/copilot-instructions.md`
- Delivery workflow in `.github/skills/api-feature-delivery/`

## Environment prep
1. Install .NET 8 SDK.
2. Open the repository root in VS Code.
3. Verify the baseline passes:
   ```powershell
   dotnet test SupportApi.slnx
   ```

## Typical workflow
1. Open Copilot Chat and ask for a new endpoint using the existing project patterns.
2. Invoke the feature delivery skill.
3. Review the resulting code with the API Reviewer agent.
4. Run the prompt file `/review-api-change` on the changed file.

## Demo objective
Show how instructions, skills, agents, and prompt files reduce manual repetition while preserving standards for validation, security, and testing.
