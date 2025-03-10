namespace SGH.Data.Interfaces;

/// <summary>
/// Интерфейс сущности
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
}