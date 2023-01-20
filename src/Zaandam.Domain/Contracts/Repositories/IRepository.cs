namespace Zaandam.Domain.Contracts.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id);
    Task AddAsync(TEntity obj);
}