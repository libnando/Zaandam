namespace Zaandam.Domain.DTOs.Responses;

public class DocumentDiffResponse
{ 
    public DocumentDiffResponse(bool equal, bool equalSize, IEnumerable<ChunkDiffResponse> diffs)
    {
        Equal = equal;
        EqualSize = equalSize;
        Diffs = diffs;
    }

    public bool Equal { get; private set; }

    public bool EqualSize { get; private set; }
    public IEnumerable<ChunkDiffResponse> Diffs { get; private set; }
}