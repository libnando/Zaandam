namespace Zaandam.Domain.Contracts.UnitsOfWork;

public interface IUnitOfWork
{
    void Commit();
    Task CommitAsync();
}