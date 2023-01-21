namespace Zaandam.Domain.DTOs.Responses;

/// <summary>
/// Default api data response.
/// </summary>
public class ZResponse<TResponseModel> where TResponseModel : class
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="data">The data.</param>
    public ZResponse(IEnumerable<TResponseModel> data)
    {
        Data = data;
        Errors = Array.Empty<ErrorResponse>();
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="data">The data.</param>
    public ZResponse(TResponseModel data)
    {
        Data = new List<TResponseModel>() { data };
        Errors = Array.Empty<ErrorResponse>();
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="errors">The errors.</param>
    public ZResponse(IEnumerable<ErrorResponse> errors)
    {
        Errors = errors;
        Data = Array.Empty<TResponseModel>();
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="error">The error.</param>
    public ZResponse(ErrorResponse error)
    {
        Errors = new List<ErrorResponse>() { error };
        Data = Array.Empty<TResponseModel>();
    }

    /// <summary>
    /// The data.
    /// </summary>
    public IEnumerable<TResponseModel> Data { get; private set; }

    /// <summary>
    /// The errors.
    /// </summary>
    public IEnumerable<ErrorResponse> Errors { get; private set; }
}
