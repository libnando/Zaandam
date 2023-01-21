using Microsoft.EntityFrameworkCore;
using Zaandam.Domain.Contracts.Repositories;
using Zaandam.Domain.Models;
using Zaandam.Infrastructure.Contexts;

namespace Zaandam.Infrastructure.Repositories;

/// <summary>
/// Class of the repository base.
/// </summary>
public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    protected readonly SqlContext Context;
    protected readonly DbSet<TEntity> DbSet;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="context">Sql context.</param>
    public Repository(SqlContext context)
    {
        Context = context;
        DbSet = Context.Set<TEntity>();
    }

    /// <summary>
    /// Add document.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    public virtual async Task AddAsync(TEntity entity) => await DbSet.AddAsync(entity);
}