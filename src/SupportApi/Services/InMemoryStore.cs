using System.Collections.Concurrent;
using SupportApi.Models;

namespace SupportApi.Services;

public class InMemoryStore
{
    private readonly ConcurrentDictionary<Guid, Feedback> _feedback = new();
    private readonly ConcurrentDictionary<Guid, Ticket> _tickets = new();

    public Feedback AddFeedback(CreateFeedbackRequest request)
    {
        var feedback = new Feedback(
            Guid.NewGuid(),
            request.CustomerEmail,
            request.Category,
            request.Comments,
            DateTimeOffset.UtcNow
        );
        _feedback[feedback.Id] = feedback;
        return feedback;
    }

    public IEnumerable<Feedback> GetAllFeedback() => _feedback.Values;

    public Ticket AddTicket(CreateTicketRequest request)
    {
        var ticket = new Ticket(
            Guid.NewGuid(),
            request.CustomerEmail,
            request.Title,
            request.Description,
            request.Priority,
            DateTimeOffset.UtcNow
        );
        _tickets[ticket.Id] = ticket;
        return ticket;
    }

    public IEnumerable<Ticket> GetAllTickets() => _tickets.Values;
}
