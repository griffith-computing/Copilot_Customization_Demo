// Remediated TicketEndpoints.cs: Logging fixed to only record TicketId and Priority
using Microsoft.AspNetCore.Http.HttpResults;
using SupportApi.Models;
using SupportApi.Services;

namespace SupportApi.Endpoints;

public static class TicketEndpoints
{
    private static readonly HashSet<string> AllowedPriorities = ["low", "medium", "high", "urgent"];

    public static RouteGroupBuilder MapTicketEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/tickets")
            .WithTags("Tickets");

        group.MapPost("/", CreateTicket);
        group.MapGet("/", (InMemoryStore store) => TypedResults.Ok(store.GetAllTickets()));

        return group;
    }

    public static Results<Created<Ticket>, ValidationProblem> CreateTicket(
        CreateTicketRequest request,
        InMemoryStore store,
        ILoggerFactory loggerFactory)
    {
        var logger = loggerFactory.CreateLogger(typeof(TicketEndpoints));
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.CustomerEmail) || !request.CustomerEmail.Contains('@'))
        {
            errors[nameof(request.CustomerEmail)] = ["A valid customer email is required."];
        }

        if (string.IsNullOrWhiteSpace(request.Title) || request.Title.Length < 3)
        {
            errors[nameof(request.Title)] = ["Title must be at least 3 characters long."];
        }

        if (string.IsNullOrWhiteSpace(request.Description) || request.Description.Length < 10)
        {
            errors[nameof(request.Description)] = ["Description must be at least 10 characters long."];
        }

        if (string.IsNullOrWhiteSpace(request.Priority) || !AllowedPriorities.Contains(request.Priority.ToLowerInvariant()))
        {
            errors[nameof(request.Priority)] = ["Priority must be one of: low, medium, high, urgent."];
        }

        if (errors.Count > 0)
        {
            logger.LogWarning("Ticket validation failed with {Count} errors.", errors.Count);
            return TypedResults.ValidationProblem(errors);
        }

        var ticket = store.AddTicket(request);

        // Remediation: Log identifier and category/priority ONLY
        logger.LogInformation("Ticket {TicketId} created with priority '{Priority}'.", ticket.Id, ticket.Priority);

        return TypedResults.Created($"/api/tickets/{ticket.Id}", ticket);
    }
}
