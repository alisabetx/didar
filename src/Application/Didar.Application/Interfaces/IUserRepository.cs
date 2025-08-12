using Didar.Domain.Entities;

namespace Didar.Application.Interfaces;

public interface IUserRepository
{
    User? Get(int id);
    void Upsert(User user);
    IRepositoryTransaction BeginTransaction(int userId);
}
