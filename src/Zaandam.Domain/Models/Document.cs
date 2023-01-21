using Zaandam.Domain.Enums;

namespace Zaandam.Domain.Models;

/// <summary>
/// Document entity.
/// </summary>
public class Document : Entity
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="key">Key of the document.</param>
    /// <param name="data">Data (base64) of the document.</param>
    /// <param name="position">The position of document (left/right).</param>
    public Document(string key, string data, DocPositionEnum position)
    {
        Key = key;
        Data = data;
        Position = position;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    private Document()
    {
    }

    /// <summary>
    /// Key of the document.
    /// </summary>
    public string Key { get; private set; } = null!;

    /// <summary>
    /// Data (base64) of the document.
    /// </summary>
    public string Data { get; private set; } = null!;

    /// <summary>
    /// The position of document (left/right).
    /// </summary>
    public DocPositionEnum Position { get; private set; }
}