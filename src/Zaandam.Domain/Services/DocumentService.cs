using Zaandam.Domain.Contracts.Repositories;
using Zaandam.Domain.Contracts.Services;
using Zaandam.Domain.Contracts.UnitsOfWork;
using Zaandam.Domain.DTOs.Responses;
using Zaandam.Domain.Enums;
using Zaandam.Domain.Extensions;
using Zaandam.Domain.Models;

namespace Zaandam.Domain.Services;

public class DocumentService : IDocumentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDocumentRepository _documentRepository;
    
    public DocumentService(IUnitOfWork unitOfWork, IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ZResponse<DocumentResponse>> Create(string key, DocPositionEnum docPosition, string data)
    {
        var document = new Document(key, data, docPosition);
        var (isDocumentValid, errors) = ValidateDocument(document);

        if (!isDocumentValid)
        {
            return new ZResponse<DocumentResponse>(errors);
        }

        await _documentRepository.AddAsync(document);
        await _unitOfWork.CommitAsync();

        var documentResponse = new List<DocumentResponse>() { new DocumentResponse(document) };

        return new ZResponse<DocumentResponse>(documentResponse);
    }

    public async Task<ZResponse<DocumentDiffResponse>> GetDiff(string key)
    {
        var docs = await _documentRepository.AllByKeyAsync(key);
        var docLeft = docs.FirstOrDefault(doc => doc.Position == DocPositionEnum.Left);
        var docRight = docs.FirstOrDefault(doc => doc.Position == DocPositionEnum.Right);

        if (docLeft is null || docRight is null)
        {
            return new ZResponse<DocumentDiffResponse>(new ErrorResponse("Falha ao recuperar documentos"));
        }

        //TODO
        var diffs = Array.Empty<ChunkDiffResponse>();
        var equal = true;
        var equalSize = true;

        return new ZResponse<DocumentDiffResponse>(new DocumentDiffResponse(equal, equalSize, diffs));
    }

    private static (bool IsValid, IEnumerable<ErrorResponse> errors) ValidateDocument(Document document)
    {
        var errors = new List<ErrorResponse>();
        var keyMaxChars = 50;

        if ($"{document.Key}".Length > keyMaxChars)
        {
            errors.Add(new ErrorResponse($"Quantidade caracteres do campo `{nameof(document.Key)}` deve ser menor que {keyMaxChars}."));
        }            

        if (string.IsNullOrWhiteSpace(document.Data))
        {
            errors.Add(new ErrorResponse($"Campo `{nameof(document.Data)}` inválido"));
        }            

        if (!document.Data.IsBase64String())
        {
            errors.Add(new ErrorResponse($"Campo `{nameof(document.Data)}` não é valor base64 válido."));
        }            

        return (!errors.Any(), errors);
    }
}