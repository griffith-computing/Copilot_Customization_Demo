namespace SupportApi.Models;

public record Ticket(Guid Id, string CustomerEmail, string Title, string Description, string Priority, DateTimeOffset CreatedAt);

public record CreateTicketRequest(string CustomerEmail, string Title, string Description, string Priority);
