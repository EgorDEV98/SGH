using SGH.Data.Interfaces;

namespace SGH.Data.Entities;

/// <summary>
/// Устройство
/// </summary>
public class Device : IEntityDate, ISoftDeleteEntity
{
    /// <summary>
    /// Идентификатор устройства
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Имя устройства
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Датчики устройства
    /// </summary>
    public ICollection<Sensor>? Sensors { get; set; }
    
    /// <summary>
    /// Актуаторы
    /// </summary>
    public ICollection<Actuator>? Actuators { get; set; }
    
    /// <summary>
    /// События устройства
    /// </summary>
    public ICollection<Event>? Events { get; set; }
    
    /// <summary>
    /// Навигационное поле
    /// </summary>
    public User User { get; set; }
    
    /// <summary>
    /// Вншний ключ
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Дата добавления устройства
    /// </summary>
    public DateTime CreatedDate { get; set; }
    
    /// <summary>
    /// Дата последнего обновления
    /// </summary>
    public DateTime LastUpdated { get; set; }
    
    /// <summary>
    /// Признак мягкого удаления
    /// </summary>
    public bool IsDeleted { get; set; }
    
    /// <summary>
    /// Дата удаления
    /// </summary>
    public DateTime DeleteDate { get; set; }
}