using Zaandam.Domain.Enums;
using Zaandam.Domain.Models;

namespace Zaandam.Domain.DTOs.Responses;

public class DocumentResponse
{ 
    public DocumentResponse(Document document)
    {
        Id = document.Id;
        Key = document.Key;
        Position = document.Position.ToString();
        Data = document.Data;
    }

    public Guid Id { get; private set; }
    public string Key { get; private set; }
    public string Position { get; private set; }
    public string Data { get; private set; }
}