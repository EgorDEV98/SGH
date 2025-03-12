using SGH.Data.Enums;

namespace SGH.Application.Models.Params;

/// <summary>
/// Модель обновления актуатора
/// </summary>
public class UpdateActuatorParams
{
    /// <summary>
    /// Идентификатор актуатора
    /// </summary>
    public required Guid Id { get; set; }
    
    /// <summary>
    /// Имя актуатора
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Состояние актуатора
    /// </summary>
    public ActuatorState? State { get; set; }
}