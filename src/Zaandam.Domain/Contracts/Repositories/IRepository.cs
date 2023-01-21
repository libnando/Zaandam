namespace Zaandam.Domain.Contracts.Repositories;

/// <summary>
/// Interface of the repository base.
/// </summary>
public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Add document.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    Task AddAsync(TEntity entity);
}