using Zaandam.Domain.Contracts.UnitsOfWork;
using Zaandam.Infrastructure.Contexts;

namespace Zaandam.Infrastructure.UnitsOfWork;

/// <summary>
/// The unit of work db context.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly SqlContext _context;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="context">Sql context.</param>
    public UnitOfWork(SqlContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Commit the db transactions.
    /// </summary>
    public async Task CommitAsync() => await _context.SaveChangesAsync();
}