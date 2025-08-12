using Didar.Application.Services;
using System.Net.Http.Json;

namespace Didar.Infrastructure.Services;

public class PackagingServiceClient(HttpClient httpClient) : IPackagingService
{
    public async Task UpgradeSubscriptionAsync(int userId, int newLevel)
    {
        var response = await httpClient.PostAsJsonAsync("api/subscriptions/upgrade", new { userId, newLevel });
        response.EnsureSuccessStatusCode();
    }

    public async Task RollbackSubscriptionAsync(int userId, int previousLevel)
    {
        var response = await httpClient.PostAsJsonAsync("api/subscriptions/rollback", new { userId, previousLevel });
        response.EnsureSuccessStatusCode();
    }

    public async Task<bool> CheckHealthAsync()
    {
        try
        {
            var response = await httpClient.GetAsync("api/subscriptions/health");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
