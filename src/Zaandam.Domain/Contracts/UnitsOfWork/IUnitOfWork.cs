namespace Zaandam.Domain.Contracts.UnitsOfWork;

/// <summary>
/// The unit of work db context.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Commit the db transactions.
    /// </summary>
    Task CommitAsync();
}