namespace SGH.Application.Models.Params;

/// <summary>
/// Модель добавления параметров
/// </summary>
public class AddDeviceParams
{
    /// <summary>
    /// Id пользователя из JWT
    /// </summary>
    public required Guid UserId { get; set; }
    
    /// <summary>
    /// Имя устройства
    /// </summary>
    public string? Name { get; set; }
}