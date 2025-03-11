namespace SGH.Application.Models.Params;

/// <summary>
/// Модель получения всех устройств пользователя
/// </summary>
public class GetDevicesParams
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public required Guid UserId { get; set; }
}