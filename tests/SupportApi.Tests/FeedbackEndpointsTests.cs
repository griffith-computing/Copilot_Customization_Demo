using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using SupportApi.Models;
using Xunit;

namespace SupportApi.Tests;

public class FeedbackEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public FeedbackEndpointsTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateFeedback_WithValidPayload_Returns201Created()
    {
        var request = new CreateFeedbackRequest(
            CustomerEmail: "alice@example.com",
            Category: "billing",
            Comments: "Invoice receipt was generated smoothly."
        );

        var response = await _client.PostAsJsonAsync("/api/feedback", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);

        var body = await response.Content.ReadFromJsonAsync<Feedback>();
        Assert.NotNull(body);
        Assert.NotEqual(Guid.Empty, body.Id);
        Assert.Equal("billing", body.Category);
    }

    [Fact]
    public async Task CreateFeedback_WithInvalidEmail_Returns400ValidationProblem()
    {
        var request = new CreateFeedbackRequest(
            CustomerEmail: "invalid-email-address",
            Category: "billing",
            Comments: "Short note"
        );

        var response = await _client.PostAsJsonAsync("/api/feedback", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);
    }
}
