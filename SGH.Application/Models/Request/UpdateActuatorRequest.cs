using SGH.Data.Enums;

namespace SGH.Application.Models.Request;

/// <summary>
/// Модель запроса обновления актуатора
/// </summary>
public class UpdateActuatorRequest
{
    /// <summary>
    /// Имя актуатора
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Состояние актуатора
    /// </summary>
    public ActuatorState? State { get; set; }
}