namespace SGH.Application.Models.Params;

/// <summary>
/// Модель получения списка
/// </summary>
public class GetSensorValuesParams
{
    /// <summary>
    /// Идентификатор датчика
    /// </summary>
    public required Guid Id { get; set; }
    
    /// <summary>
    /// Дата начала выборки
    /// </summary>
    public DateTime? From { get; set; }
    
    /// <summary>
    /// Дата окончания выборки
    /// </summary>
    public DateTime? To { get; set; }
    
    /// <summary>
    /// Отступ
    /// </summary>
    public int? Offset { get; set; }
    
    /// <summary>
    /// Лимит
    /// </summary>
    public int? Limit { get; set; }
}