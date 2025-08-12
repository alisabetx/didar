using Didar.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Didar.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly UserSubscriptionService _service;

    public HealthController(UserSubscriptionService service)
    {
        _service = service;
    }

    [HttpGet("packaging")]
    public async Task<IActionResult> Packaging()
    {
        var healthy = await _service.CheckPackagingHealthAsync();
        return healthy ? Ok("Packaging service reachable") : StatusCode(503, "Packaging service unreachable");
    }
}
