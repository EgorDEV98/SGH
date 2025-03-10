using SGH.Data.Enums;

namespace SGH.Data.Entities;

/// <summary>
/// Актуаторы
/// </summary>
public class Actuator
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Имя актуатора
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Системное имя актуатора
    /// </summary>
    public string SystemName { get; set; }
    
    /// <summary>
    /// Текущее состояние
    /// </summary>
    public ActuatorState State { get; set; }
    
    /// <summary>
    /// Навигационное поле
    /// </summary>
    public Device Device { get; set; }
    
    /// <summary>
    /// Внешний ключ
    /// </summary>
    public Guid DeviceId { get; set; }
}