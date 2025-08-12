using Didar.Application.Repositories;

namespace Didar.Application.Services;

public class UserSubscriptionService(IUserRepository userRepository, IPackagingService packagingService)
{
    public async Task UpgradeSubscriptionAsync(int userId, int newLevel, bool failLocal = false)
    {
        var user = userRepository.Get(userId) ?? new Domain.User(userId, 0);
        userRepository.Upsert(user);
        var previousLevel = user.SubscriptionLevel;

        using var transaction = userRepository.BeginTransaction(userId);
        bool packagingApplied = false;

        try
        {
            user.SubscriptionLevel = newLevel;
            userRepository.Upsert(user);

            await packagingService.UpgradeSubscriptionAsync(userId, newLevel);
            packagingApplied = true;

            if (failLocal)
            {
                throw new InvalidOperationException("Simulated local failure");
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            if (packagingApplied)
            {
                await packagingService.RollbackSubscriptionAsync(userId, previousLevel);
            }
            throw;
        }
    }

    public Task<bool> CheckPackagingHealthAsync() => packagingService.CheckHealthAsync();
}
