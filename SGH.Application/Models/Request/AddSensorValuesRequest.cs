namespace SGH.Application.Models.Request;

/// <summary>
/// Модель запрос добавления показания датчика
/// </summary>
public class AddSensorValuesRequest
{
    /// <summary>
    /// Значения датчика
    /// </summary>
    public required float Value { get; set; }
    
    /// <summary>
    /// Дата измерения
    /// </summary>
    public required DateTime MeasurementDate { get; set; }
}