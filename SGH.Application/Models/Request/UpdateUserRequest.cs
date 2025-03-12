namespace SGH.Application.Models.Request;

/// <summary>
/// Модель обновления пользователя
/// </summary>
public class UpdateUserRequest
{
    /// <summary>
    /// Пароль
    /// </summary>
    public string? Password { get; set; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string? Name { get; set; }
}