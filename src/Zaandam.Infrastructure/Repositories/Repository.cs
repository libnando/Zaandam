using Microsoft.EntityFrameworkCore;
using Zaandam.Domain.Contracts.Repositories;
using Zaandam.Infrastructure.Contexts;

namespace Zaandam.Infrastructure.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly SqlContext Context;
    protected readonly DbSet<TEntity> DbSet;

    public Repository(SqlContext context)
    {
        Context = context;
        DbSet = Context.Set<TEntity>();
    }

    public virtual async Task AddAsync(TEntity obj) => await DbSet.AddAsync(obj);

    public virtual async Task<TEntity?> GetByAsync(string id) => await DbSet.FindAsync(id);
         
    public void SaveChanges() => Context.SaveChanges();    

    public async Task SaveChangesAsync() => await Context.SaveChangesAsync();
}