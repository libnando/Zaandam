namespace Zaandam.Domain.Extensions;

public static class StringExtensions
{
    public static bool IsBase64String(this string data) => Convert.TryFromBase64String(data, new Span<byte>(new byte[data.Length]), out _);
}