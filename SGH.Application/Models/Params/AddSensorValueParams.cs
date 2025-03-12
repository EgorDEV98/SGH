namespace SGH.Application.Models.Params;

/// <summary>
/// Модель добавления значения датчика
/// </summary>
public class AddSensorValueParams
{
    /// <summary>
    /// Идентификатор датчика
    /// </summary>
    public required Guid Id { get; set; }
    
    /// <summary>
    /// Значения датчика
    /// </summary>
    public required float Value { get; set; }
    
    /// <summary>
    /// Дата измерения
    /// </summary>
    public required DateTime MeasurementDate { get; set; }
}