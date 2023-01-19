using Zaandam.Domain.Contracts.Repositories;
using Zaandam.Domain.Contracts.Services;
using Zaandam.Domain.Models;

namespace Zaandam.Domain.Services;

public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _documentRepository;
    
    public DocumentService(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task AddAsync(Document obj) => await _documentRepository.AddAsync(obj);
    public async Task<Document?> GetByAsync(string id) => await _documentRepository.GetByAsync(id);
    public async Task SaveChangesAsync() => await _documentRepository.SaveChangesAsync();
}