using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using SupportApi.Models;
using Xunit;

namespace SupportApi.Tests;

public class TicketEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public TicketEndpointsTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateTicket_WithValidPayload_Returns201Created()
    {
        var request = new CreateTicketRequest(
            CustomerEmail: "support-user@example.com",
            Title: "Unable to reset password",
            Description: "Password reset link sends a 404 page when clicked.",
            Priority: "high"
        );

        var response = await _client.PostAsJsonAsync("/api/tickets", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);

        var body = await response.Content.ReadFromJsonAsync<Ticket>();
        Assert.NotNull(body);
        Assert.NotEqual(Guid.Empty, body.Id);
        Assert.Equal("high", body.Priority);
    }

    [Fact]
    public async Task CreateTicket_WithInvalidPayload_Returns400ValidationProblem()
    {
        var request = new CreateTicketRequest(
            CustomerEmail: "bad-email",
            Title: "x",
            Description: "short",
            Priority: "unknown-priority"
        );

        var response = await _client.PostAsJsonAsync("/api/tickets", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);
    }
}
