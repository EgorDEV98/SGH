using System.ComponentModel.DataAnnotations;

namespace SGH.Application.Models.Responces;

/// <summary>
/// Модель ответа регистрации
/// </summary>
public class RegistrationResponse
{
    /// <summary>
    /// Токен
    /// </summary>
    [Required]
    public required string Token { get; set; }
}