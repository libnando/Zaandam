using Zaandam.Domain.Contracts.Repositories;
using Zaandam.Domain.Contracts.Services;
using Zaandam.Domain.Contracts.UnitsOfWork;
using Zaandam.Domain.DTOs.Responses;
using Zaandam.Domain.Enums;
using Zaandam.Domain.Extensions;
using Zaandam.Domain.Helpers;
using Zaandam.Domain.Models;

namespace Zaandam.Domain.Services;

/// <summary>
/// Class to handle document services.
/// </summary>
public class DocumentService : IDocumentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDocumentRepository _documentRepository;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="unitOfWork">Unit of work db context.</param>
    /// <param name="documentRepository">Document repository.</param>
    public DocumentService(IUnitOfWork unitOfWork, IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Create the document.
    /// </summary>
    /// <param name="key">Key of the document.</param>
    /// <param name="docPosition">The position of document (left/right).</param>
    /// <param name="data">Data of the document.</param>
    /// <returns>Document response data.</returns>
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

    /// <summary>
    /// Get the diffs of the documents.
    /// </summary>
    /// <param name="key">Key of the document.</param>
    /// <returns>The diffs of the documents.</returns>
    public async Task<ZResponse<DocumentDiffResponse>> GetDiff(string key)
    {
        var docs = await _documentRepository.AllByKeyAsync(key);
        var docLeft = docs.FirstOrDefault(doc => doc.Position == DocPositionEnum.Left);
        var docRight = docs.FirstOrDefault(doc => doc.Position == DocPositionEnum.Right);

        if (docLeft is null || docRight is null)
        {
            return new ZResponse<DocumentDiffResponse>(new ErrorResponse("Falha ao recuperar documentos"));
        }

        var (equalsData, equalsSize, size, offsetDiffs) = DocumentCompareHelper.Compare(docLeft.Data, docRight.Data);
        var response = new DocumentDiffResponse(equalsData, equalsSize, size, offsetDiffs);

        return new ZResponse<DocumentDiffResponse>(response);
    }

    /// <summary>
    /// Validate the document.
    /// </summary>
    /// <param name="document">The document to validate.</param>
    /// <returns>Validation data.</returns>
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