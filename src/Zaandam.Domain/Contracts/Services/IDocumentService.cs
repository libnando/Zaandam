using Zaandam.Domain.DTOs.Responses;
using Zaandam.Domain.Enums;
using Zaandam.Domain.Models;

namespace Zaandam.Domain.Contracts.Services;

public interface IDocumentService : IService<Document>
{
    Task<ZResponse<DocumentResponse>> Create(string key, DocPositionEnum docPosition, string data);
    Task<ZResponse<DocumentDiffResponse>> GetDiff(string key);
}