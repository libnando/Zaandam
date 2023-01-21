using Zaandam.Domain.Models;

namespace Zaandam.Domain.Contracts.Repositories;

/// <summary>
/// Interface of the document repository.
/// </summary>
public interface IDocumentRepository : IRepository<Document>
{
    /// <summary>
    /// Get documents by key.
    /// </summary>
    /// <param name="key">Document key.</param>
    Task<IEnumerable<Document>> AllByKeyAsync(string key);
}