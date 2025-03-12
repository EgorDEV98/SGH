namespace SGH.Application.Models.Params;

/// <summary>
/// Модель получения всех актуаторов устройства
/// </summary>
public class GetActuatorsParams
{
    /// <summary>
    /// Идентификатор устройства
    /// </summary>
    public required Guid DeviceId { get; set; }
}