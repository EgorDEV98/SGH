namespace SGH.Application.Models.Params;

/// <summary>
/// Модель регистрации
/// </summary>
public class RegistrationParams
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Логин
    /// </summary>
    public required string Login  { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public required string Password { get; set; }
}