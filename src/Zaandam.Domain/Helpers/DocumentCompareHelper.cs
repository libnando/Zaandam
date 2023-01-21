namespace Zaandam.Domain.Helpers;

/// <summary>
/// Helper class to compare documents.
/// </summary>
public static class DocumentCompareHelper
{
    /// <summary>
    /// Compare two base64 files.
    /// </summary>
    /// <param name="base64File1">Document 1 base64 data.</param>
    /// <param name="base64File2">Document 2 base64 data.</param>
    /// <returns>Comparasion data.</returns>
    public static (bool EqualsData, bool EqualsSize, long Size, long[] OffsetDiffs) Compare(string base64File1, string base64File2)
    {
        var file1Stream = new MemoryStream(Convert.FromBase64String(base64File1));
        var file2Stream = new MemoryStream(Convert.FromBase64String(base64File2));
        var equalsData = $"{base64File1}".Trim().Equals($"{base64File2}".Trim());
        var equalsSize = file1Stream.Length == file2Stream.Length;
        var size = file1Stream.Length;        
        var offsetDiffs = new List<long>();

        if (equalsData || !equalsSize)
        {
            return (equalsData, equalsSize, size, Array.Empty<long>());
        }

        int file1byte;
        int file2byte;

        do
        {
            file1byte = file1Stream.ReadByte();
            file2byte = file2Stream.ReadByte();

            if (file1byte != file2byte)
            {
                offsetDiffs.Add(file2Stream.Position);
            }
        }
        while (file1byte != -1);

        return (equalsData, equalsSize, size, offsetDiffs.ToArray());
    }
}