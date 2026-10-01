// Reference Endpoint Pattern for SupportApi
using Microsoft.AspNetCore.Http.HttpResults;
using SupportApi.Models;
using SupportApi.Services;

namespace SupportApi.Endpoints;

public static class ExampleEndpoints
{
    public static RouteGroupBuilder MapExampleEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/examples")
            .WithTags("Examples");

        group.MapPost("/", CreateExample);

        return group;
    }

    public static Results<Created<ExampleItem>, ValidationProblem> CreateExample(
        CreateExampleRequest request,
        InMemoryStore store,
        ILoggerFactory loggerFactory)
    {
        var logger = loggerFactory.CreateLogger(typeof(ExampleEndpoints));
        var errors = new Dictionary<string, string[]>();

        // 1. Validation
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            errors[nameof(request.Title)] = ["Title is required."];
        }

        if (errors.Count > 0)
        {
            logger.LogWarning("Example validation failed with {Count} errors.", errors.Count);
            return TypedResults.ValidationProblem(errors);
        }

        // 2. State Mutation
        var item = store.AddExample(request);

        // 3. Secure Logging (Identifier only, no sensitive payload)
        logger.LogInformation("Example item {ItemId} created successfully.", item.Id);

        // 4. Typed Created Result
        return TypedResults.Created($"/api/examples/{item.Id}", item);
    }
}

public record ExampleItem(Guid Id, string Title, DateTimeOffset CreatedAt);
public record CreateExampleRequest(string Title);
