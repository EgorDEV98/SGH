using System.ComponentModel.DataAnnotations;

namespace SGH.Application.Models.Responces;

/// <summary>
/// Модель ответа
/// </summary>
public class AuthResponse
{
    /// <summary>
    /// Токен
    /// </summary>
    [Required]
    public required string Token { get; set; }
}