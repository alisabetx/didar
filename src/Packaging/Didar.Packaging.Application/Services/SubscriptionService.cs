using Didar.Packaging.Application.Interfaces;
using Didar.Packaging.Domain.Entities;

namespace Didar.Packaging.Application.Services;

public class SubscriptionService
{
    private readonly ISubscriptionRepository _repository;

    public SubscriptionService(ISubscriptionRepository repository)
    {
        _repository = repository;
    }

    public Task UpgradeAsync(int userId, int newLevel)
    {
        using var tx = _repository.BeginTransaction(userId);
        var subscription = _repository.Get(userId) ?? new Subscription(userId, 0);
        subscription.Level = newLevel;
        _repository.Upsert(subscription);
        tx.Commit();
        return Task.CompletedTask;
    }

    public Task RollbackAsync(int userId, int previousLevel)
    {
        using var tx = _repository.BeginTransaction(userId);
        var subscription = _repository.Get(userId) ?? new Subscription(userId, 0);
        subscription.Level = previousLevel;
        _repository.Upsert(subscription);
        tx.Commit();
        return Task.CompletedTask;
    }
}
