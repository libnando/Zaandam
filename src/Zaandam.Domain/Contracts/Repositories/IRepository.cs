namespace Zaandam.Domain.Contracts.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity obj);
    Task<TEntity?> GetByAsync(string id);
    Task SaveChangesAsync();
    void SaveChanges();
}