using Didar.Packaging.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Didar.Packaging.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly SubscriptionService _service;

    public SubscriptionsController(SubscriptionService service)
    {
        _service = service;
    }

    [HttpPost("upgrade")]
    public async Task<IActionResult> Upgrade(UpgradeRequest request)
    {
        await _service.UpgradeAsync(request.UserId, request.NewLevel);
        return Ok();
    }

    [HttpPost("rollback")]
    public async Task<IActionResult> Rollback(RollbackRequest request)
    {
        await _service.RollbackAsync(request.UserId, request.PreviousLevel);
        return Ok();
    }

    [HttpGet("health")]
    public IActionResult Health() => Ok();
}

public record UpgradeRequest(int UserId, int NewLevel);
public record RollbackRequest(int UserId, int PreviousLevel);
