namespace SGH.Application.Models.Params;

/// <summary>
/// Модель получения датчика
/// </summary>
public class GetSensorParams
{
    /// <summary>
    /// Идентификатор датчика
    /// </summary>
    public required Guid Id { get; set; }
}