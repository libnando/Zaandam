using Zaandam.Domain.Models;

namespace Zaandam.Domain.DTOs.Responses;

/// <summary>
/// Document response DTO.
/// </summary>
public class DocumentResponse
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="document">The document.</param>
    public DocumentResponse(Document document)
    {
        Key = document.Key;
        Position = document.Position.ToString();
        Data = document.Data;
    }

    /// <summary>
    /// Document Key.
    /// </summary>
    public string Key { get; private set; }
    
    /// <summary>
    /// Document position.
    /// </summary>
    public string Position { get; private set; }

    /// <summary>
    /// Document data.
    /// </summary>
    public string Data { get; private set; }
}