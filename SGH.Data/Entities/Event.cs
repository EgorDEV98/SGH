using SGH.Data.Enums;
using SGH.Data.Interfaces;

namespace SGH.Data.Entities;

/// <summary>
/// Событие
/// </summary>
public class Event : IEntityDate
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Наименование события
    /// </summary>
    public EventName EventName { get; set; }
    
    /// <summary>
    /// Тип события
    /// </summary>
    public EventType EventType { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedDate { get; set; }
    
    /// <summary>
    /// Послднее обновление
    /// </summary>
    public DateTime LastUpdated { get; set; }
    
    /// <summary>
    /// Навигационное поле
    /// </summary>
    public Device Device { get; set; }
    
    /// <summary>
    /// Внешний ключ
    /// </summary>
    public Guid DeviceId { get; set; }
}