using System.ComponentModel.DataAnnotations;

namespace SGH.Application.Models.Request;

/// <summary>
/// Модель регистрации
/// </summary>
public class RegistrationRequest
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    [Required]
    public required string Name { get; set; }
    
    /// <summary>
    /// Логин
    /// </summary>
    [Required]
    public required string Login  { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    [Required]
    public required string Password { get; set; }
}