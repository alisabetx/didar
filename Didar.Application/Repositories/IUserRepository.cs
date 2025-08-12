using Didar.Domain;

namespace Didar.Application.Repositories;

public interface IUserRepository
{
    User? Get(int id);
    void Upsert(User user);
    IRepositoryTransaction BeginTransaction(int userId);
}
