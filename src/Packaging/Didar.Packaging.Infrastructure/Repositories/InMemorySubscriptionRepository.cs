using Didar.Packaging.Application.Repositories;
using Didar.Packaging.Domain;

namespace Didar.Packaging.Infrastructure.Repositories;

public class InMemorySubscriptionRepository : ISubscriptionRepository
{
    private readonly Dictionary<int, Subscription> _subs = [];

    public Subscription? Get(int userId)
        => _subs.TryGetValue(userId, out var s) ? new Subscription(s.UserId, s.Level) : null;

    public void Upsert(Subscription subscription)
        => _subs[subscription.UserId] = new Subscription(subscription.UserId, subscription.Level);

    public IRepositoryTransaction BeginTransaction(int userId)
        => new InMemoryTransaction(this, userId);

    private class InMemoryTransaction(InMemorySubscriptionRepository repo, int userId) : IRepositoryTransaction
    {
        private readonly Subscription? _snapshot = repo.Get(userId);
        private bool _completed;

        public void Commit() => _completed = true;

        public void Rollback()
        {
            if (_snapshot is null)
                repo._subs.Remove(userId);
            else
                repo._subs[userId] = _snapshot;
            _completed = true;
        }

        public void Dispose()
        {
            if (!_completed)
            {
                Rollback();
            }
        }
    }
}
