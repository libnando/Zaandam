using Zaandam.Domain.Models;

namespace Zaandam.Domain.Contracts.Repositories;

public interface IDocumentRepository : IRepository<Document>
{
    Task<IEnumerable<Document>> AllByKeyAsync(string key);
}