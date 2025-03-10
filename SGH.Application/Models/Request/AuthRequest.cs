using System.ComponentModel.DataAnnotations;

namespace SGH.Application.Models.Request;

/// <summary>
/// Модель запроса авторизации
/// </summary>
public class AuthRequest
{
    /// <summary>
    /// Логин
    /// </summary>
    [Required]
    public required string Login { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    [Required]
    public required string Password { get; set; }
}