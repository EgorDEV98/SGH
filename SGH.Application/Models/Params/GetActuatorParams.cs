namespace SGH.Application.Models.Params;

/// <summary>
/// Модель получения актуатора
/// </summary>
public class GetActuatorParams
{
    /// <summary>
    /// Идентификатор актуатора
    /// </summary>
    public required Guid Id { get; set; }
}