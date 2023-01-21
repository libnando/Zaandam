using Zaandam.Domain.DTOs.Responses;
using Zaandam.Domain.Enums;
using Zaandam.Domain.Models;

namespace Zaandam.Domain.Contracts.Services;

/// <summary>
/// Interface of the document service.
/// </summary>
public interface IDocumentService : IService<Document>
{
    /// <summary>
    /// Create the document.
    /// </summary>
    /// <param name="key">Key of the document.</param>
    /// <param name="docPosition">The position of document (left/right).</param>
    /// <param name="data">Data of the document.</param>
    /// <returns>Document response data.</returns>
    Task<ZResponse<DocumentResponse>> Create(string key, DocPositionEnum docPosition, string data);

    /// <summary>
    /// Get the diffs of the documents.
    /// </summary>
    /// <param name="key">Key of the document.</param>
    /// <returns>The diffs of the documents.</returns>
    Task<ZResponse<DocumentDiffResponse>> GetDiff(string key);
}