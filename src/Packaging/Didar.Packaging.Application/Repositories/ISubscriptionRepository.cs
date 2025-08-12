using Didar.Packaging.Domain;

namespace Didar.Packaging.Application.Repositories;

public interface ISubscriptionRepository
{
    Subscription? Get(int userId);
    void Upsert(Subscription subscription);
    IRepositoryTransaction BeginTransaction(int userId);
}
