namespace SGH.Application.Models.Request;

/// <summary>
/// Модель добавления датчика
/// </summary>
public class AddSensorRequest
{
    /// <summary>
    /// Имя датчика
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Системное имя датчика
    /// </summary>
    public required string SystemName { get; set; }
}