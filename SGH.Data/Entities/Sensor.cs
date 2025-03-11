using SGH.Data.Interfaces;

namespace SGH.Data.Entities;

/// <summary>
/// Датчик
/// </summary>
public class Sensor : IEntityDate, ISoftDeleteEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Имя датчика
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Системное имя датчика
    /// </summary>
    public string SystemName { get; set; }
    
    /// <summary>
    /// Показания датчика
    /// </summary>
    public ICollection<SensorValue>? Values { get; set; }
    
    /// <summary>
    /// Навигационное поле
    /// </summary>
    public Device Device { get; set; }
    
    /// <summary>
    /// Внешний ключ
    /// </summary>
    public Guid DeviceId { get; set; }

    /// <summary>
    /// Дата добавления датчика
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