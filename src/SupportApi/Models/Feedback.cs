namespace SupportApi.Models;

public record Feedback(Guid Id, string CustomerEmail, string Category, string Comments, DateTimeOffset CreatedAt);

public record CreateFeedbackRequest(string CustomerEmail, string Category, string Comments);
