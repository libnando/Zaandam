namespace Zaandam.Domain.Extensions;

/// <summary>
/// String extensions.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Verify if the data are base64 kind.
    /// </summary>
    /// <param name="data">The string data.</param>
    /// <returns>If the data are base64.</returns>
    public static bool IsBase64String(this string data) => Convert.TryFromBase64String(data, new Span<byte>(new byte[data.Length]), out _);
}