using Didar.Application.Interfaces;
using Didar.Domain.Entities;

namespace Didar.Infrastructure.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private readonly Dictionary<int, User> _users = new();

    public User? Get(int id) => _users.TryGetValue(id, out var user) ? new User(user.Id, user.SubscriptionLevel) : null;

    public void Upsert(User user) => _users[user.Id] = new User(user.Id, user.SubscriptionLevel);

    public IRepositoryTransaction BeginTransaction(int userId) => new InMemoryTransaction(this, userId);

    private class InMemoryTransaction : IRepositoryTransaction
    {
        private readonly InMemoryUserRepository _repository;
        private readonly int _userId;
        private readonly User? _snapshot;
        private bool _completed;

        public InMemoryTransaction(InMemoryUserRepository repository, int userId)
        {
            _repository = repository;
            _userId = userId;
            _snapshot = repository.Get(userId);
        }

        public void Commit() => _completed = true;

        public void Rollback()
        {
            if (_snapshot is null)
                _repository._users.Remove(_userId);
            else
                _repository._users[_userId] = _snapshot;
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
