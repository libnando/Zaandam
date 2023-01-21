namespace Zaandam.Domain.DTOs.Responses;

/// <summary>
/// Error default class.
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="message">The error message.</param>
    public ErrorResponse(string message)
    {
        Message = message;
    }

    /// <summary>
    /// The error message.
    /// </summary>
    public string Message { get; private set; }
}