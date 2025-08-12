using Didar.Packaging.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Didar.Packaging.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionsController(SubscriptionService subscriptionService) : ControllerBase
{
    [HttpPost("upgrade")]
    public async Task<IActionResult> Upgrade(UpgradeRequest request)
    {
        await subscriptionService.UpgradeAsync(request.UserId, request.NewLevel);
        return Ok();
    }

    [HttpPost("rollback")]
    public async Task<IActionResult> Rollback(RollbackRequest request)
    {
        await subscriptionService.RollbackAsync(request.UserId, request.PreviousLevel);
        return Ok();
    }

    [HttpGet("health")]
    public IActionResult Health() => Ok();
}

public record UpgradeRequest(int UserId, int NewLevel);
public record RollbackRequest(int UserId, int PreviousLevel);
