namespace Zaandam.Domain.Models;

/// <summary>
/// Entity base.
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// The identificator.
    /// </summary>
    public Guid Id { get; protected set; } = Guid.NewGuid();

    /// <summary>
    /// Date of creation of entity.
    /// </summary>
    public DateTime CreationDate { get; protected set; } = DateTime.Now;
}