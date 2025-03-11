namespace SGH.Application.Models.Params;

/// <summary>
/// Модель удаления устройства
/// </summary>
public class DeleteDeviceParams
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    public required Guid UserId { get; set; }
    
    /// <summary>
    /// Id устройства
    /// </summary>
    public required Guid DeviceId { get; set; }
}