namespace SGH.Application.Models.Params;

/// <summary>
/// Модель удаление актуатора
/// </summary>
public class DeleteActuatorParams
{
    /// <summary>
    /// Идентификатор актуатора
    /// </summary>
    public required Guid Id { get; set; }
}