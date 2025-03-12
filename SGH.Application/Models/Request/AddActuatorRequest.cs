namespace SGH.Application.Models.Request;

public class AddActuatorRequest
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