using SGH.Data.Interfaces;

namespace SGH.Data.Entities;

/// <summary>
/// Показания датчика
/// </summary>
public class SensorValue : IEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Показания датчика
    /// </summary>
    public float Value { get; set; }
    
    /// <summary>
    /// Дата измерения
    /// </summary>
    public DateTime MeasurementDate { get; set; }
    
    /// <summary>
    /// Навигационное поле
    /// </summary>
    public Sensor Sensor { get; set; }
    
    /// <summary>
    /// Внешний ключ
    /// </summary>
    public Guid SensorId { get; set; }
}