using Didar.Packaging.Application.Repositories;
using Didar.Packaging.Domain;

namespace Didar.Packaging.Infrastructure.Repositories;

public class InMemorySubscriptionRepository : ISubscriptionRepository
{
    private readonly Dictionary<int, Subscription> _subs = new();

    public Subscription? Get(int userId)
        => _subs.TryGetValue(userId, out var s) ? new Subscription(s.UserId, s.Level) : null;

    public void Upsert(Subscription subscription)
        => _subs[subscription.UserId] = new Subscription(subscription.UserId, subscription.Level);

    public IRepositoryTransaction BeginTransaction(int userId)
        => new InMemoryTransaction(this, userId);

    private class InMemoryTransaction : IRepositoryTransaction
    {
        private readonly InMemorySubscriptionRepository _repo;
        private readonly int _userId;
        private readonly Subscription? _snapshot;
        private bool _completed;

        public InMemoryTransaction(InMemorySubscriptionRepository repo, int userId)
        {
            _repo = repo;
            _userId = userId;
            _snapshot = repo.Get(userId);
        }

        public void Commit() => _completed = true;

        public void Rollback()
        {
            if (_snapshot is null)
                _repo._subs.Remove(_userId);
            else
                _repo._subs[_userId] = _snapshot;
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
