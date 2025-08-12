namespace Didar.Application.Services;

public interface IPackagingService
{
    Task UpgradeSubscriptionAsync(int userId, int newLevel);
    Task RollbackSubscriptionAsync(int userId, int previousLevel);
    Task<bool> CheckHealthAsync();
}
