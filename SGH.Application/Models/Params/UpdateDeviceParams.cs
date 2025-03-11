namespace SGH.Application.Models.Params;

/// <summary>
/// Модель обновления устройства
/// </summary>
public class UpdateDeviceParams
{
    /// <summary>
    /// Id пользователя из JWT
    /// </summary>
    public required Guid UserId { get; set; }
    
    /// <summary>
    /// Id устройства
    /// </summary>
    public required Guid DeviceId { get; set; }
    
    /// <summary>
    /// Имя устройства
    /// </summary>
    public string? Name { get; set; }
}