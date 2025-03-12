using SGH.Data.Enums;

namespace SGH.Application.Models.Responces;

/// <summary>
/// Модель ответа актуатора
/// </summary>
public class GetActuatorResponse
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Имя актуатора
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Системное имя актуатора
    /// </summary>
    public string SystemName { get; set; }
    
    /// <summary>
    /// Текущее состояние
    /// </summary>
    public ActuatorState State { get; set; }
    
    /// <summary>
    /// Дата добавления актуатора
    /// </summary>
    public DateTime CreatedDate { get; set; }
    
    /// <summary>
    /// Дата обновления актуатор
    /// </summary>
    public DateTime LastUpdated { get; set; }
}