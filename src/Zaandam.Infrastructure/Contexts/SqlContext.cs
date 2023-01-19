using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zaandam.Domain.Models;

namespace Zaandam.Infrastructure.Contexts;

public class SqlContext : DbContext
{
    public SqlContext(DbContextOptions<SqlContext> options) : base(options)
    {
    }

    public DbSet<Document> Documents => Set<Document>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}