using Microsoft.EntityFrameworkCore;
using Zaandam.Domain.Contracts.Repositories;
using Zaandam.Domain.Models;
using Zaandam.Infrastructure.Contexts;

namespace Zaandam.Infrastructure.Repositories;

/// <summary>
/// Class of the document repository.
/// </summary>
public class DocumentRepository : Repository<Document>, IDocumentRepository
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="context">Sql context.</param>
    public DocumentRepository(SqlContext context) : base(context)
    {        
    }

    /// <summary>
    /// Get documents by key.
    /// </summary>
    /// <param name="key">Document key.</param>
    public async Task<IEnumerable<Document>> AllByKeyAsync(string key) => await DbSet.Where(doc => doc.Key == key).ToListAsync();
}