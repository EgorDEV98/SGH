namespace SGH.Application.Models.Params;

/// <summary>
/// Добавление актуатора
/// </summary>
public class AddActuatorParams
{
    /// <summary>
    /// Идентификатор устройства
    /// </summary>
    public required Guid DeviceId { get; set; }
    
    /// <summary>
    /// Системное имя актуатора
    /// </summary>
    public required string SystemName { get; set; }
    
    /// <summary>
    /// Имя актуатора
    /// </summary>
    public string? Name { get; set; }
}