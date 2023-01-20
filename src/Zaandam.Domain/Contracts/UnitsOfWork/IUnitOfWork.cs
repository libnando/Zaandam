namespace Zaandam.Domain.Contracts.UnitsOfWork;

public interface IUnitOfWork
{
    Task CommitAsync();
}