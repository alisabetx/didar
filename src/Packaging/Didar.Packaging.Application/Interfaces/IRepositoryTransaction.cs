namespace Didar.Packaging.Application.Interfaces;

public interface IRepositoryTransaction : IDisposable
{
    void Commit();
    void Rollback();
}
