namespace Zaandam.Domain.DTOs.Responses;

public class ZResponse<TResponseModel> where TResponseModel : class
{
    public ZResponse(IEnumerable<TResponseModel> data)
    {
        Data = data;
        Errors = Array.Empty<ErrorResponse>();
    }

    public ZResponse(TResponseModel data)
    {
        Data = new List<TResponseModel>() { data };
        Errors = Array.Empty<ErrorResponse>();
    }

    public ZResponse(IEnumerable<ErrorResponse> errors)
    {
        Errors = errors;
        Data = Array.Empty<TResponseModel>();
    }

    public ZResponse(ErrorResponse error)
    {
        Errors = new List<ErrorResponse>() { error };
        Data = Array.Empty<TResponseModel>();
    }

    public IEnumerable<TResponseModel> Data { get; private set; }

    public IEnumerable<ErrorResponse> Errors { get; private set; }
}
