namespace Zaandam.Domain.DTOs.Responses;

/// <summary>
/// Document diff response DTO.
/// </summary>
public class DocumentDiffResponse
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="equalsData">Informs if data are equal.</param>
    /// <param name="equalsSize">Informs if size is equal.</param>
    /// <param name="size">The dize of document.</param>
    /// <param name="offsetDiffs">The offset diffs.</param>
    public DocumentDiffResponse(bool equalsData, bool equalsSize, long size, long[] offsetDiffs)
    {
        EqualsData = equalsData;
        EqualsSize = equalsSize;
        Size = size;
        OffsetDiffs = offsetDiffs;
    }

    /// <summary>
    /// Informs if data are equal.
    /// </summary>
    public bool EqualsData { get; private set; }

    /// <summary>
    /// Informs if size is equal.
    /// </summary>
    public bool EqualsSize { get; private set; }

    /// <summary>
    /// The size of document.
    /// </summary>
    public long Size { get; private set; }

    /// <summary>
    /// The offset diffs.
    /// </summary>
    public long[] OffsetDiffs { get; private set; }
}