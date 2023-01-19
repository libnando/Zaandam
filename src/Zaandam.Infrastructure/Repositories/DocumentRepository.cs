using Zaandam.Domain.Contracts.Repositories;
using Zaandam.Domain.Models;
using Zaandam.Infrastructure.Contexts;

namespace Zaandam.Infrastructure.Repositories;

public class DocumentRepository : Repository<Document>, IDocumentRepository
{
    public DocumentRepository(SqlContext context) : base(context)
    {   
    }
}