using Microsoft.EntityFrameworkCore;
using Zaandam.Domain.Contracts.Repositories;
using Zaandam.Domain.Models;
using Zaandam.Infrastructure.Contexts;

namespace Zaandam.Infrastructure.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    protected readonly SqlContext Context;
    protected readonly DbSet<TEntity> DbSet;

    public Repository(SqlContext context)
    {
        Context = context;
        DbSet = Context.Set<TEntity>();
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id) => await DbSet.FirstOrDefaultAsync(doc => doc.Id == id);

    public virtual async Task AddAsync(TEntity obj) => await DbSet.AddAsync(obj);
}