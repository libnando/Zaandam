namespace Zaandam.Domain.Contracts.Services;

public interface IService<TEntity> where TEntity : class
{
    Task AddAsync(TEntity obj);
    Task<TEntity?> GetByAsync(string id);
    Task SaveChangesAsync();
}