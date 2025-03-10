namespace SGH.Data.Interfaces;

/// <summary>
/// Интерфейс версионности
/// </summary>
public interface IEntityDate : IEntity
{
    /// <summary>
    /// Дата создания объекта
    /// </summary>
    public DateTime CreatedDate { get; set; }
    
    /// <summary>
    /// Последнее обновление
    /// </summary>
    public DateTime LastUpdated { get; set; }
}