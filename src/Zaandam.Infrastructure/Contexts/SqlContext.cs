using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zaandam.Domain.Models;

namespace Zaandam.Infrastructure.Contexts;

/// <summary>
/// Sql context.
/// </summary>
public class SqlContext : DbContext
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="options">Sql context options.</param>
    public SqlContext(DbContextOptions<SqlContext> options) : base(options)
    {
    }

    /// <summary>
    /// Set db documents context.
    /// </summary>
    public DbSet<Document> Documents => Set<Document>();

    /// <summary>
    /// EF config method.
    /// </summary>
    /// <param name="modelBuilder">Model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}