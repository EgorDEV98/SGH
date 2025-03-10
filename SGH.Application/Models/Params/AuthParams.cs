namespace SGH.Application.Models.Params;

/// <summary>
/// Модель авторизации
/// </summary>
public class AuthParams
{
    /// <summary>
    /// Логин
    /// </summary>
    public required string Login { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public required string Password { get; set; }
}