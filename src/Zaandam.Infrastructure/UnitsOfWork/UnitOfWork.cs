using Zaandam.Domain.Contracts.UnitsOfWork;
using Zaandam.Infrastructure.Contexts;

namespace Zaandam.Infrastructure.UnitsOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly SqlContext _context;

    public UnitOfWork(SqlContext context)
    {
        _context = context;
    }

    public async Task CommitAsync() => await _context.SaveChangesAsync();
}