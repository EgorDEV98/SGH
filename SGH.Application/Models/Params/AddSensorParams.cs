namespace SGH.Application.Models.Params;

/// <summary>
/// Добавить датчик
/// </summary>
public class AddSensorParams
{
    /// <summary>
    /// Идентификатор устройства
    /// </summary>
    public required Guid DeviceId { get; set; }
    
    /// <summary>
    /// Имя датчика
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Системное имя датчика
    /// </summary>
    public required string SystemName { get; set; }
}