// Example of code generated WITHOUT repository instructions
// Anti-patterns highlighted for the presentation:
// 1. Controller pattern instead of project-standard Minimal APIs
// 2. Returns raw string error bodies instead of RFC 7807 ProblemDetails
// 3. Security violation: Logs full customer description & PII email directly
// 4. Mutable class instead of record DTOs

using Microsoft.AspNetCore.Mvc;

namespace SupportApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly ILogger<TicketsController> _logger;

    public TicketsController(ILogger<TicketsController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult CreateTicket([FromBody] UninstructedTicketDto dto)
    {
        // DRIFT 1: Raw string error payload, not RFC 7807 ValidationProblem
        if (string.IsNullOrEmpty(dto.CustomerEmail) || !dto.CustomerEmail.Contains('@'))
        {
            return BadRequest("Customer email is missing or invalid.");
        }

        // DRIFT 2: Prohibited sensitive logging (PII and customer description in logs)
        _logger.LogInformation("Creating ticket for customer {Email} with description: {Description}",
            dto.CustomerEmail, dto.Description);

        var ticketId = Guid.NewGuid();

        // DRIFT 3: Raw anonymous object returned without typed results or Location header
        return StatusCode(201, new { Id = ticketId, Status = "Created" });
    }
}

public class UninstructedTicketDto
{
    public string? CustomerEmail { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Priority { get; set; }
}
