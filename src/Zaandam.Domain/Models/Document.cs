using Zaandam.Domain.Enums;

namespace Zaandam.Domain.Models;

public class Document : Entity
{
    public Document(string key, string data, DocPositionEnum position)
    {
        Key = key;
        Data = data;
        Position = position;
    }

    private Document()
    {
    }

    public string Key { get; private set; } = null!;

    public string Data { get; private set; } = null!;

    public DocPositionEnum Position { get; private set; }
}