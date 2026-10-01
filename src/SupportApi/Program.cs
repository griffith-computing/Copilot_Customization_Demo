using SupportApi.Endpoints;
using SupportApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddSingleton<InMemoryStore>();
builder.Services.AddLogging();

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

app.MapGet("/", () => Results.Ok(new { message = "Support API is running", status = "Healthy" }));
app.MapFeedbackEndpoints();

app.Run();

public partial class Program { }
