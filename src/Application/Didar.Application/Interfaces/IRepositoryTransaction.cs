namespace Didar.Application.Interfaces;

public interface IRepositoryTransaction : IDisposable
{
    void Commit();
    void Rollback();
}
