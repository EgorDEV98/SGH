namespace SGH.Application.Models.Params;

/// <summary>
///  Модель получения устройств пользователя
/// </summary>
public class GetDeviceParams
{
    /// <summary>
    /// Идентификатор пользователя из JWT
    /// </summary>
    public required Guid UserId { get; set; }
    
    /// <summary>
    /// Идентификатор устройства
    /// </summary>
    public required Guid DeviceId { get; set; }
}