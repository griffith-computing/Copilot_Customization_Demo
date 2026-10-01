using Microsoft.AspNetCore.Http.HttpResults;
using SupportApi.Models;
using SupportApi.Services;

namespace SupportApi.Endpoints;

public static class FeedbackEndpoints
{
    public static RouteGroupBuilder MapFeedbackEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/feedback")
            .WithTags("Feedback");

        group.MapPost("/", CreateFeedback);
        group.MapGet("/", (InMemoryStore store) => TypedResults.Ok(store.GetAllFeedback()));

        return group;
    }

    public static Results<Created<Feedback>, ValidationProblem> CreateFeedback(
        CreateFeedbackRequest request,
        InMemoryStore store,
        ILoggerFactory loggerFactory)
    {
        var logger = loggerFactory.CreateLogger(typeof(FeedbackEndpoints));

        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.CustomerEmail) || !request.CustomerEmail.Contains('@'))
        {
            errors[nameof(request.CustomerEmail)] = ["A valid customer email address is required."];
        }

        if (string.IsNullOrWhiteSpace(request.Category))
        {
            errors[nameof(request.Category)] = ["Category is required."];
        }

        if (string.IsNullOrWhiteSpace(request.Comments) || request.Comments.Length < 5)
        {
            errors[nameof(request.Comments)] = ["Comments must be at least 5 characters long."];
        }

        if (errors.Count > 0)
        {
            logger.LogWarning("Feedback validation failed for category '{Category}' with {ErrorCount} errors.",
                request.Category, errors.Count);
            return TypedResults.ValidationProblem(errors);
        }

        var item = store.AddFeedback(request);

        // Security rule: Never log sensitive comments or customer email; log only identifiers and category
        logger.LogInformation("Feedback {FeedbackId} created under category '{Category}'.",
            item.Id, item.Category);

        return TypedResults.Created($"/api/feedback/{item.Id}", item);
    }
}
