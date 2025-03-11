namespace SGH.Application.Models.Params;

/// <summary>
/// Модель обновления данных пользователя
/// </summary>
public class UpdateUserParams
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public required Guid UserId { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string? Password { get; set; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string? Name { get; set; }
}