using Didar.Packaging.Domain.Entities;

namespace Didar.Packaging.Application.Interfaces;

public interface ISubscriptionRepository
{
    Subscription? Get(int userId);
    void Upsert(Subscription subscription);
    IRepositoryTransaction BeginTransaction(int userId);
}
