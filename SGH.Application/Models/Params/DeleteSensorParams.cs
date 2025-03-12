namespace SGH.Application.Models.Params;

/// <summary>
/// Модель удаления датчика
/// </summary>
public class DeleteSensorParams
{
    /// <summary>
    /// Идентификатор датчика
    /// </summary>
    public required Guid Id { get; set; }
}