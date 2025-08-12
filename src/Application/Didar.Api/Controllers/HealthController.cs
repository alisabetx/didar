using Didar.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Didar.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController(UserSubscriptionService service) : ControllerBase
{
    [HttpGet("packaging")]
    public async Task<IActionResult> Packaging()
    {
        var healthy = await service.CheckPackagingHealthAsync();
        return healthy ? Ok("Packaging service reachable") : StatusCode(503, "Packaging service unreachable");
    }
}
