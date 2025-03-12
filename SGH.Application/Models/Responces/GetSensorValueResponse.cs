namespace SGH.Application.Models.Responces;

/// <summary>
/// Модель ответа получения показаний датчиков
/// </summary>
public class GetSensorValueResponse
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Показания датчика
    /// </summary>
    public float Value { get; set; }
    
    /// <summary>
    /// Дата измерения
    /// </summary>
    public DateTime MeasurementDate { get; set; }
}