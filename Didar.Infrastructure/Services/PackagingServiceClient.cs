using System.Net.Http.Json;
using Didar.Application.Services;

namespace Didar.Infrastructure.Services;

public class PackagingServiceClient : IPackagingService
{
    private readonly HttpClient _httpClient;

    public PackagingServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task UpgradeSubscriptionAsync(int userId, int newLevel)
    {
        var response = await _httpClient.PostAsJsonAsync("api/subscriptions/upgrade", new { userId, newLevel });
        response.EnsureSuccessStatusCode();
    }

    public async Task RollbackSubscriptionAsync(int userId, int previousLevel)
    {
        var response = await _httpClient.PostAsJsonAsync("api/subscriptions/rollback", new { userId, previousLevel });
        response.EnsureSuccessStatusCode();
    }

    public async Task<bool> CheckHealthAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/subscriptions/health");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
