// Reference Integration Test Template for SupportApi
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace SupportApi.Tests;

public class ExampleEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ExampleEndpointsTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Post_WithValidPayload_Returns201Created()
    {
        var request = new { Title = "Valid Sample Title" };

        var response = await _client.PostAsJsonAsync("/api/examples", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);
    }

    [Fact]
    public async Task Post_WithInvalidPayload_Returns400ValidationProblem()
    {
        var request = new { Title = "" };

        var response = await _client.PostAsJsonAsync("/api/examples", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);
    }
}
