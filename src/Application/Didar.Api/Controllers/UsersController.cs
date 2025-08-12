using Didar.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Didar.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(UserSubscriptionService service) : ControllerBase
{
    [HttpPost("{id}/subscription/upgrade")]
    public async Task<IActionResult> Upgrade(int id, UpgradeRequest request)
    {
        await service.UpgradeSubscriptionAsync(id, request.NewLevel, request.FailLocal);
        return Ok();
    }
}

public record UpgradeRequest(int NewLevel, bool FailLocal = false);
