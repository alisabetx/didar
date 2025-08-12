using Didar.Packaging.Application.Repositories;
using Didar.Packaging.Domain;

namespace Didar.Packaging.Application.Services;

public class SubscriptionService(ISubscriptionRepository repository)
{
    public Task UpgradeAsync(int userId, int newLevel)
    {
        using var tx = repository.BeginTransaction(userId);
        var subscription = repository.Get(userId) ?? new Subscription(userId, 0);
        subscription.Level = newLevel;
        repository.Upsert(subscription);
        tx.Commit();
        return Task.CompletedTask;
    }

    public Task RollbackAsync(int userId, int previousLevel)
    {
        using var tx = repository.BeginTransaction(userId);
        var subscription = repository.Get(userId) ?? new Subscription(userId, 0);
        subscription.Level = previousLevel;
        repository.Upsert(subscription);
        tx.Commit();
        return Task.CompletedTask;
    }
}
