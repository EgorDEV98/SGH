namespace SGH.Application.Models.Params;

/// <summary>
/// Модель получения датчиков устройства
/// </summary>
public class GetSensorsParams
{
    /// <summary>
    /// Id устройства
    /// </summary>
    public required Guid DeviceId { get; set; }
}