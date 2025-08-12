using Didar.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Didar.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserSubscriptionService _service;

    public UsersController(UserSubscriptionService service)
    {
        _service = service;
    }

    [HttpPost("{id}/subscription/upgrade")]
    public async Task<IActionResult> Upgrade(int id, UpgradeRequest request)
    {
        await _service.UpgradeSubscriptionAsync(id, request.NewLevel, request.FailLocal);
        return Ok();
    }
}

public record UpgradeRequest(int NewLevel, bool FailLocal = false);
